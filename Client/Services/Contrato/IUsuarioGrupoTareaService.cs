using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IUsuarioGrupoTareaService
    {
        Task<List<UsuarioGrupoTareaDTO>> ListarUsuarioGrupoTareas();
        Task<UsuarioGrupoTareaDTO> ObtenerUsuarioGrupoTarea(int id);
        Task<int> AgregarUsuarioGrupoTarea(UsuarioGrupoTareaDTO UsuarioGrupoTarea);
        Task<bool> EliminarUsuarioGrupoTarea(int idUsuario, int idTarea, int idGrupo, int idProyecto);
    }
}
