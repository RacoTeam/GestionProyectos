using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> ListarUsuarios();
        Task<UsuarioDTO> ObtenerUsuario(int id);
        Task<int> AgregarUsuario(UsuarioDTO Usuario);
        Task<bool> EliminarUsuario(int id);
    }
}
