using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClienteDTO>> ListarClientes()
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

        public async Task<int> AgregarModificarCliente(int idCliente, ClienteDTO cliente)
        {
            HttpResponseMessage result;

            if (cliente.IdCliente == cliente.IdCliente)
            {
                // Si el cliente tiene un Id, se trata de una modificación
                result = await _httpClient.PutAsJsonAsync($"api/Cliente/{idCliente}", cliente);
            }
            else
            {
                // Si el cliente no tiene un Id válido, se trata de una adición
                result = await _httpClient.PostAsJsonAsync("api/Cliente", cliente);
            }

            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
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
