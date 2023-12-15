using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Shared.Models;
using System.Net.Http.Json;

namespace GestionProyectos.Client.Services.Implementacion
{
    public class ProyectoService : IProyectoService
    {
        private readonly HttpClient _httpClient;
        public ProyectoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProyectoDTO>> ListarProyectos()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<ProyectoDTO>>>("api/Proyecto/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<ProyectoDTO> ObtenerProyecto(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<ProyectoDTO>>($"api/Proyecto/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarProyecto(ProyectoDTO Proyecto)
        {
            Proyecto.IdClienteNavigation = null;
            Proyecto.IdUsuarioNavigation = null;

            var result = await _httpClient.PostAsJsonAsync("api/Proyecto", Proyecto);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> AgregarModificarProyecto(int idProyecto, ProyectoDTO proyectoDTO)
        {
            proyectoDTO.IdClienteNavigation = null;
            proyectoDTO.IdUsuarioNavigation = null;

            HttpResponseMessage result;

            if (proyectoDTO.IdProyecto != 0)
            {
                // Si el proyecto tiene un Id, se trata de una modificación
                result = await _httpClient.PutAsJsonAsync($"api/Proyecto/{idProyecto}", proyectoDTO);
            }
            else
            {
                result = await _httpClient.PostAsJsonAsync("api/Proyecto", proyectoDTO);
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


        public async Task<bool> EliminarProyecto(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Proyecto/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
