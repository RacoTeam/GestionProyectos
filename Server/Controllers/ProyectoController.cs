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
                    if (proyecto.IdClienteNavigation != null)
                    {
                        proyecto.IdClienteNavigation.Proyectos = null;
                    }

                    proyecto.IdUsuarioNavigation = _dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == proyecto.IdUsuario);
                    
                    if (proyecto.IdUsuarioNavigation != null)
                    {
                        proyecto.IdUsuarioNavigation.Proyectos = null;
                    }

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
                    dbProyecto.IdClienteNavigation = _dbContext.Clientes.FirstOrDefault(c => c.IdCliente == dbProyecto.IdCliente);
                    if (dbProyecto.IdClienteNavigation != null)
                    {
                        dbProyecto.IdClienteNavigation.Proyectos = null;
                    }

                    dbProyecto.IdUsuarioNavigation = _dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == dbProyecto.IdUsuario);
                    if (dbProyecto.IdUsuarioNavigation != null)
                    {
                        dbProyecto.IdUsuarioNavigation.Proyectos = null;
                    }

                    proyectoDTO = _mapper.Map<ProyectoDTO>(dbProyecto);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = proyectoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
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
        public async Task<ActionResult> AgregarProyecto(ProyectoDTO proyectoDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                proyectoDTO.IdClienteNavigation = null;
                proyectoDTO.IdUsuarioNavigation = null;
                proyectoDTO.Tareas = null;
                proyectoDTO.Grupos = null;

                var dbProyecto = _mapper.Map<Proyecto>(proyectoDTO);

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
                    responseApi.Mensaje = "Proyecto no guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarProyecto(int id, ProyectoDTO proyectoDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos
                    .Include(p => p.IdClienteNavigation)
                    .Include(p => p.IdUsuarioNavigation)
                    .Where(p => p.IdProyecto == id)
                    .FirstOrDefaultAsync();

                if (dbProyecto == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbProyecto with values from proyectoDTO
                _mapper.Map(proyectoDTO, dbProyecto);

                // Clear related collections to avoid unintended updates
                dbProyecto.IdClienteNavigation = null;
                dbProyecto.IdUsuarioNavigation = null;
                dbProyecto.Tareas = null;
                dbProyecto.Grupos = null;

                _dbContext.Entry(dbProyecto).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbProyecto.IdProyecto;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi); // Internal Server Error
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("hard/{idProyecto}")]
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
                    responseApi.Mensaje = "Proyecto no encontrado";
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
        [Route("soft/{idProyecto}")]
        public async Task<IActionResult> EliminadoLogicoProyecto(int idProyecto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos
                    .Include(p => p.Tareas)
                    .Include(p => p.Grupos)
                    .FirstOrDefaultAsync(f => f.IdProyecto == idProyecto);

                if (dbProyecto != null)
                {
                    dbProyecto.FechaEliminacion = DateTime.Now;

                    foreach (var tarea in dbProyecto.Tareas)
                    {
                        tarea.FechaEliminacion = DateTime.Now;
                    }

                    foreach (var grupo in dbProyecto.Grupos)
                    {
                        grupo.FechaEliminacion = DateTime.Now;
                    }

                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
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
        [Route("restore/{idProyecto}")]
        public async Task<IActionResult> RestaurarProyecto(int idProyecto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(f => f.IdProyecto == idProyecto);

                if (dbProyecto != null)
                {
                    var fechaEliminacionProyecto = dbProyecto.FechaEliminacion;

                    // Restaurar el proyecto
                    dbProyecto.FechaEliminacion = null;
                    await _dbContext.SaveChangesAsync();

                    // Restaurar las tareas asociadas al proyecto y que fueron eliminadas en la misma fecha
                    var tareasEliminadas = await _dbContext.Tareas
                        .Where(t => t.IdProyecto == idProyecto && t.FechaEliminacion == fechaEliminacionProyecto)
                        .ToListAsync();

                    foreach (var tarea in tareasEliminadas)
                    {
                        tarea.FechaEliminacion = null;
                    }

                    // Restaurar los grupos asociados al proyecto y que fueron eliminados en la misma fecha
                    var gruposEliminados = await _dbContext.Grupos
                        .Where(g => g.IdProyecto == idProyecto && g.FechaEliminacion == fechaEliminacionProyecto)
                        .ToListAsync();

                    foreach (var grupo in gruposEliminados)
                    {
                        grupo.FechaEliminacion = null;
                    }

                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
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
