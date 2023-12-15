using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class TareaService : ITareaService
    {
        private readonly HttpClient _httpClient;
        public TareaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TareaDTO>> ListarTareas()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<TareaDTO>>>("api/Tarea/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<TareaDTO> ObtenerTarea(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<TareaDTO>>($"api/Tarea/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarTarea(TareaDTO tarea, UsuarioGrupoTareaDTO? usuarioGrupoTarea = null)
        {

            var result = await _httpClient.PostAsJsonAsync("api/Tarea", tarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (usuarioGrupoTarea != null)
            {
                // Si se proporciona un UsuarioGrupoTareaDTO, agrégalo a la lista de UsuarioGrupoTareas en la tareaDTO
                tarea.UsuarioGrupoTareas = new List<UsuarioGrupoTareaDTO> { usuarioGrupoTarea };

            }

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> EliminarTarea(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Tarea/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
