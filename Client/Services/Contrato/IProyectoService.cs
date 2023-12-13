using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IProyectoService
    {
        Task<List<ProyectoDTO>> ListarProyectos();
        Task<ProyectoDTO> ObtenerProyecto(int id);
        Task<int> AgregarProyecto(ProyectoDTO Proyecto);
        Task<bool> EliminarProyecto(int id);
    }
}
