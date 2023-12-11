using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<ClienteDTO>>>("api/Cliente/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<ClienteDTO> ObtenerCliente(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<ClienteDTO>>($"api/Cliente/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarCliente(ClienteDTO Cliente)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Cliente", Cliente);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Cliente/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
