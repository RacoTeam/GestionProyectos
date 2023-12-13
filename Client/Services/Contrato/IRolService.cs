using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IRolService
    {
        Task<List<RolDTO>> ListarRols();
        Task<RolDTO> ObtenerRol(int id);
        Task<int> AgregarRol(RolDTO Rol);
        Task<bool> EliminarRol(int id);
    }
}
