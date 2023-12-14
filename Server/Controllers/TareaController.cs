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
    public class TareaController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public TareaController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarTareas()
        {
            var responseApi = new ResponseAPI<List<TareaDTO>>();
            var listaTareasDTO = new List<TareaDTO>();
            try
            {
                var tareasDb = await _dbContext.Tareas.ToListAsync();
                foreach (var tarea in tareasDb)
                {
                    tarea.IdProyectoNavigation = await _dbContext.Proyectos.FirstOrDefaultAsync(p => p.IdProyecto == tarea.IdProyecto);
                    tarea.IdProyectoNavigation.IdClienteNavigation = null;
                    tarea.IdProyectoNavigation.IdUsuarioNavigation = null;
                    tarea.IdProyectoNavigation.Tareas = null;

                    listaTareasDTO.Add(_mapper.Map<TareaDTO>(tarea));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareasDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpGet]
        [Route("{idTarea}")]
        public async Task<ActionResult> ObtenerTarea(int idTarea)
        {
            var responseApi = new ResponseAPI<TareaDTO>();
            var tareaDTO = new TareaDTO();

            try
            {
                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(f => f.IdTarea == idTarea);

                if (dbTarea != null)
                {

                    dbTarea.IdProyectoNavigation = await _dbContext.Proyectos.FirstOrDefaultAsync(p => p.IdProyecto == dbTarea.IdProyecto);
                    dbTarea.IdProyectoNavigation.IdClienteNavigation = null;
                    dbTarea.IdProyectoNavigation.IdUsuarioNavigation = null;
                    dbTarea.IdProyectoNavigation.Tareas = null;


                    tareaDTO = _mapper.Map<TareaDTO>(dbTarea);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = tareaDTO;
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
        public async Task<ActionResult> AgregarTarea(TareaDTO tareaDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                tareaDTO.IdProyectoNavigation = null;
                tareaDTO.UsuarioGrupoTareas = null;
                tareaDTO.Recursos = null;

                var dbTarea = _mapper.Map<Tarea>(tareaDTO);

                _dbContext.Tareas.Add(dbTarea);
                await _dbContext.SaveChangesAsync();

                if (dbTarea.IdTarea != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.IdTarea;
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
        [Route("{idTarea}")]
        public async Task<IActionResult> EliminarTarea(int idTarea)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(f => f.IdTarea == idTarea);

                if (dbTarea != null)
                {
                    _dbContext.Tareas.Remove(dbTarea);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Tarea no encontrada";
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
