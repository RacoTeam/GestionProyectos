using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IUsuarioGrupoGrupoService
    {
        Task<List<UsuarioGrupoDTO>> ListarUsuarioGrupos(int idUsuarioGrupo);
        Task<UsuarioGrupoDTO> ObtenerUsuarioGrupo(int id);
        Task<int> AgregarUsuarioGrupo(UsuarioGrupoDTO UsuarioGrupo);
        Task<bool> EliminarUsuarioGrupo(int id);
    }
}
