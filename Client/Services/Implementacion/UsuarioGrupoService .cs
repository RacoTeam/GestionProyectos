using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class UsuarioGrupoService : IUsuarioGrupoService
    {
        private readonly HttpClient _httpClient;
        public UsuarioGrupoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioGrupoDTO>> ListarUsuarioGrupos()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<UsuarioGrupoDTO>>>("api/UsuarioGrupo/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<UsuarioGrupoDTO> ObtenerUsuarioGrupo(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<UsuarioGrupoDTO>>($"api/UsuarioGrupo/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarUsuarioGrupo(UsuarioGrupoDTO UsuarioGrupo)
        {
            var result = await _httpClient.PostAsJsonAsync("api/UsuarioGrupo", UsuarioGrupo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarUsuarioGrupo(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/UsuarioGrupo/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
