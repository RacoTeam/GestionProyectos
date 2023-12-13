using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class RolService : IRolService
    {
        private readonly HttpClient _httpClient;
        public RolService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RolDTO>> ListarRols()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<RolDTO>>>("api/Rol/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<RolDTO> ObtenerRol(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<RolDTO>>($"api/Rol/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarRol(RolDTO Rol)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Rol", Rol);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarRol(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Rol/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
