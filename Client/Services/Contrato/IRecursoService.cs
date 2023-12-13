using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IRecursoService
    {
        Task<List<RecursoDTO>> ListarRecurso(int idRecurso);
        Task<RecursoDTO> ObtenerRecurso(int id);
        Task<int> AgregarRecurso(RecursoDTO Recurso);
        Task<bool> EliminarRecurso(int id);
    }
}
