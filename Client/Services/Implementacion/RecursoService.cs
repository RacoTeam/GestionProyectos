using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class RecursoService : IRecursoService
    {
        private readonly HttpClient _httpClient;
        public RecursoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RecursoDTO>> ListarRecurso()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<RecursoDTO>>>("api/Recurso/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<RecursoDTO> ObtenerRecurso(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<RecursoDTO>>($"api/Recurso/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarRecurso(RecursoDTO Recurso)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Recurso", Recurso);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarRecurso(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Recurso/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
