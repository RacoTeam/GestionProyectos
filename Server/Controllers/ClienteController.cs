using AutoMapper;
using GestionProyectos.Server.Data;
using GestionProyectos.Server.Models;
using GestionProyectos.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public ClienteController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarClientes()
        {
            var responseApi = new ResponseAPI<List<ClienteDTO>>();
            var listaClientesDTO = new List<ClienteDTO>();
            try
            {
                var ClienteDb = await _dbContext.Clientes.ToListAsync();
                foreach (var Cliente in ClienteDb)
                {
                    Cliente.Proyectos = null;
                    listaClientesDTO.Add(_mapper.Map<ClienteDTO>(Cliente));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaClientesDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpGet]
        [Route("{idCliente}")]
        public async Task<ActionResult> ObtenerCliente(int idCliente)
        {
            var responseApi = new ResponseAPI<ClienteDTO>();
            var ClienteDTO = new ClienteDTO();

            try
            {
                var dbCliente = await _dbContext.Clientes.FirstOrDefaultAsync(f => f.IdCliente == idCliente);

                if (dbCliente != null)
                {
                    ClienteDTO = _mapper.Map<ClienteDTO>(dbCliente);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ClienteDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarCliente(ClienteDTO ClienteDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                //ClienteDTO.Usuarios = null;
                ClienteDTO.Proyectos = null;
                var dbCliente = _mapper.Map<Cliente>(ClienteDTO);

                _dbContext.Clientes.Add(dbCliente);
                await _dbContext.SaveChangesAsync();

                if (dbCliente.IdCliente != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCliente.IdCliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("{idCliente}")]
        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbCliente = await _dbContext.Clientes.FirstOrDefaultAsync(f => f.IdCliente == idCliente);

                if (dbCliente != null)
                {
                    _dbContext.Clientes.Remove(dbCliente);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrada";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
