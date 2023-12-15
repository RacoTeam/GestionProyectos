using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface ITareaService
    {
        Task<List<TareaDTO>> ListarTareas();
        Task<TareaDTO> ObtenerTarea(int id);
        Task<int> AgregarTarea(TareaDTO tarea, UsuarioGrupoTareaDTO? usuarioGrupoTarea = null);
        Task<bool> EliminarTarea(int id);
    }
}
