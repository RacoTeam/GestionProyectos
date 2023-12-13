using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IUsuarioGrupoService
    {
        Task<List<UsuarioGrupoDTO>> ListarUsuarioGrupos();
        Task<UsuarioGrupoDTO> ObtenerUsuarioGrupo(int id);
        Task<int> AgregarUsuarioGrupo(UsuarioGrupoDTO UsuarioGrupo);
        Task<bool> EliminarUsuarioGrupo(int id);
    }
}
