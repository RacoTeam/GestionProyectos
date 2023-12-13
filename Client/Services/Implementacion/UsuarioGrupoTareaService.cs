using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class UsuarioGrupoTareaService : IUsuarioGrupoTareaService
    {
        private readonly HttpClient _httpClient;
        public UsuarioGrupoTareaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioGrupoTareaDTO>> ListarUsuarioGrupoTareas()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<UsuarioGrupoTareaDTO>>>("api/UsuarioGrupoTarea/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<UsuarioGrupoTareaDTO> ObtenerUsuarioGrupoTarea(int idUsuario)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<UsuarioGrupoTareaDTO>>($"api/UsuarioGrupoTarea/{idUsuario}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarUsuarioGrupoTarea(UsuarioGrupoTareaDTO UsuarioGrupoTarea)
        {
            var result = await _httpClient.PostAsJsonAsync("api/UsuarioGrupoTarea", UsuarioGrupoTarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarUsuarioGrupoTarea(int idUsuario, int idTarea, int idGrupo, int idProyecto)
        {
            var result = await _httpClient.DeleteAsync($"api/UsuarioGrupoTarea/?idUsuario={idUsuario}&idTarea?={idTarea}&idTarea?={idGrupo}&idTarea?={idProyecto}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
