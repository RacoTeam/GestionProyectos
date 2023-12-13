using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class Recursoervice
    {
        private readonly HttpClient _httpClient;
        public Recursoervice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioDTO>> ListarRecurso()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<UsuarioDTO>>>("api/Usuario/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<UsuarioDTO> ObtenerUsuario(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<UsuarioDTO>>($"api/Usuario/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarUsuario(UsuarioDTO Usuario)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Usuario", Usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Usuario/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
