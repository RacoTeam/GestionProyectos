using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface ITareaService
    {
        Task<List<TareaDTO>> ListarTareas(int idTarea);
        Task<TareaDTO> ObtenerTarea(int id);
        Task<int> AgregarTarea(TareaDTO Tarea);
        Task<bool> EliminarTarea(int id);
    }
}
