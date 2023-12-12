using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using GestionProyectos.Server.Models;
using GestionProyectos.Server.Data;
using GestionProyectos.Shared.Models;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace GestionProyectos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;


        public ProyectoController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarProyectos()
        {
            var responseApi = new ResponseAPI<List<ProyectoDTO>>();
            var listaProyectosDTO = new List<ProyectoDTO>();
            try
            {
                var proyectosDb = await _dbContext.Proyectos.ToListAsync();
                foreach (var proyecto in proyectosDb)
                {
                    proyecto.IdClienteNavigation = _dbContext.Clientes.FirstOrDefault(c => c.IdCliente == proyecto.IdCliente);
                    proyecto.IdClienteNavigation.Proyectos = null;

                    proyecto.IdUsuarioNavigation = _dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == proyecto.IdUsuario);
                    proyecto.IdUsuarioNavigation.Proyectos = null;


                    listaProyectosDTO.Add(_mapper.Map<ProyectoDTO>(proyecto));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProyectosDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("{idProyecto}")]
        public async Task<ActionResult> ObtenerProyecto(int idProyecto)
        {
            var responseApi = new ResponseAPI<ProyectoDTO>();
            var proyectoDTO = new ProyectoDTO();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(f => f.IdProyecto == idProyecto);

                if (dbProyecto != null)
                {
                    dbProyecto.IdClienteNavigation = _dbContext.Clientes.FirstOrDefault(c=>c.IdCliente == dbProyecto.IdCliente);
                    dbProyecto.IdClienteNavigation.Proyectos = null;

                    dbProyecto.IdUsuarioNavigation = _dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == dbProyecto.IdUsuario);
                    dbProyecto.IdUsuarioNavigation.Proyectos = null;

                    proyectoDTO = _mapper.Map<ProyectoDTO>(dbProyecto);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = proyectoDTO;
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
        public async Task<ActionResult> AgregarProyecto(ProyectoDTO Proyecto)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbProyecto = new Proyecto
                {
                    Descripcion = Proyecto.Descripcion,
                    Nombre = Proyecto.Nombre,
                    FechaInicio = Proyecto.FechaInicio,
                    FechaFin = Proyecto.FechaFin,
                    IdCliente = Proyecto.IdCliente,
                    //TODO
                };

                _dbContext.Proyectos.Add(dbProyecto);
                await _dbContext.SaveChangesAsync();

                if (dbProyecto.IdProyecto != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.IdProyecto;
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
        [Route("{idProyecto}")]
        public async Task<IActionResult> EliminarProyecto(int idProyecto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(f => f.IdProyecto == idProyecto);

                if (dbProyecto != null)
                {
                    _dbContext.Proyectos.Remove(dbProyecto);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrada";
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
