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
    public class TareaController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        public TareaController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TareaDTO>>();
            var listaTareasDTO = new List<TareaDTO>();
            try
            {
                foreach (var item in await _dbContext.Tareas.Include(d => d.IdTarea).ToListAsync())
                {
                    listaTareasDTO.Add(new TareaDTO
                    {
                        IdTarea = item.IdTarea,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Avance = item.Avance,
                        IdProyecto = item.IdProyecto,
                        //TODO
                    });
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
            var TareaDTO = new TareaDTO();

            try
            {
                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(f => f.IdTarea == idTarea);

                if (dbTarea != null)
                {
                    TareaDTO.IdTarea = idTarea;
                    TareaDTO.Nombre = dbTarea.Nombre;
                    TareaDTO.Descripcion = dbTarea.Descripcion;
                    TareaDTO.FechaInicio = dbTarea.FechaInicio;
                    TareaDTO.FechaFin = dbTarea.FechaFin;
                    TareaDTO.Avance = dbTarea.Avance;
                    TareaDTO.IdProyecto = dbTarea.IdProyecto;
                    //TODO

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TareaDTO;
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
        public async Task<ActionResult> AgregarTarea(TareaDTO Tarea)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbTarea = new Tarea
                {
                    Nombre = Tarea.Nombre,
                    Descripcion = Tarea.Descripcion,
                    FechaInicio = Tarea.FechaInicio,
                    FechaFin = Tarea.FechaFin,
                    Avance = Tarea.Avance,
                    IdProyecto = Tarea.IdProyecto,
                    //TODO
                };

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
