using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IRolService
    {
        Task<List<RolDTO>> ListarRols(int idRol);
        Task<RolDTO> ObtenerRol(int id);
        Task<int> AgregarRol(RolDTO Rol);
        Task<bool> EliminarRol(int id);
    }
}
