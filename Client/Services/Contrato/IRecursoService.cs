using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IRecursoService
    {
        Task<List<RecursoDTO>> ListarRecurso();
        Task<RecursoDTO> ObtenerRecurso(int id);
        Task<int> AgregarRecurso(RecursoDTO Recurso);
        Task<int> AgregarModificarRecurso(int idRecurso, RecursoDTO recurso);
        Task<bool> EliminarRecurso(int id);
    }
}
