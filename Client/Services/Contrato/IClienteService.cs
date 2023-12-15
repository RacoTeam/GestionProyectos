using GestionProyectos.Shared.Models;

namespace GestionProyectos.Client.Services.Contrato
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ListarClientes();
        Task<ClienteDTO> ObtenerCliente(int id);
        Task<int> AgregarCliente(ClienteDTO Cliente);
        Task<int> AgregarModificarCliente(int idCliente, ClienteDTO cliente);
        Task<bool> EliminarCliente(int id);
    }
}
