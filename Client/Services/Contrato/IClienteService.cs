using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ListarCLientes(int idUsuario);
        Task<ClienteDTO> ObtenerCliente(int id);
        Task<int> AgregarCliente(ClienteDTO Cliente);
        Task<bool> EliminarCliente(int id);
    }
}
