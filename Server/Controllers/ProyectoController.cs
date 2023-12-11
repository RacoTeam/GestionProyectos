using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using GestionProyectos.Server.Models;
using GestionProyectos.Server.Data;
using GestionProyectos.Shared.Models;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;


namespace GestionProyectos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public ProyectoController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProyectoDTO>>();
            var listaProyectosDTO = new List<ProyectoDTO>();
            try
            {
                foreach (var item in await _dbContext.Proyectos.Include(d => d.IdProyecto).ToListAsync())
                {
                    listaProyectosDTO.Add(new ProyectoDTO
                    {
                        IdProyecto = item.IdProyecto,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        IdCliente = item.IdCliente,
                        //TODO
                    });
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
            var ProyectoDTO = new ProyectoDTO();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(f => f.IdProyecto == idProyecto);

                if (dbProyecto != null)
                {
                    ProyectoDTO.IdProyecto = idProyecto;
                    ProyectoDTO.Nombre = dbProyecto.Nombre;
                    ProyectoDTO.Descripcion = dbProyecto.Descripcion;

                    ProyectoDTO.FechaInicio = dbProyecto.FechaInicio;
                    ProyectoDTO.FechaFin = dbProyecto.FechaFin;
                    ProyectoDTO.IdCliente = dbProyecto.IdCliente;
                    //TODO

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ProyectoDTO;
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
