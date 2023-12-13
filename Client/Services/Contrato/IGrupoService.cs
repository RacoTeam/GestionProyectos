using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IGrupoService
    {
        Task<List<GrupoDTO>> ListarGrupos();
        Task<GrupoDTO> ObtenerGrupo(int id);
        Task<int> AgregarGrupo(GrupoDTO Grupo);
        Task<bool> EliminarGrupo(int id);
    }
}
