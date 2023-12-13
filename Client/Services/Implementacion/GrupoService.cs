using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class GrupoService : IGrupoService
    {
        private readonly HttpClient _httpClient;
        public GrupoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GrupoDTO>> ListarGrupos()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<GrupoDTO>>>("api/Grupo/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<GrupoDTO> ObtenerGrupo(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<GrupoDTO>>($"api/Grupo/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarGrupo(GrupoDTO Grupo)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Grupo", Grupo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarGrupo(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Grupo/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
