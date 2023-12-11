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
    public class RecursoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public RecursoController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RecursoDTO>>();
            var listaRecursosDTO = new List<RecursoDTO>();
            try
            {
                foreach (var item in await _dbContext.Recursos.Include(d => d.IdRecurso).ToListAsync())
                {
                    listaRecursosDTO.Add(new RecursoDTO
                    {
                        IdRecurso = item.IdRecurso,
                        Nombre = item.Nombre,
                        CostoDia = item.CostoDia,
                        Tipo = item.Tipo,
                        IdTarea = item.IdTarea,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRecursosDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("{idRecurso}")]
        public async Task<ActionResult> ObtenerRecurso(int idRecurso)
        {
            var responseApi = new ResponseAPI<RecursoDTO>();
            var RecursoDTO = new RecursoDTO();

            try
            {
                var dbRecurso = await _dbContext.Recursos.FirstOrDefaultAsync(f => f.IdRecurso == idRecurso);

                if (dbRecurso != null)
                {
                    RecursoDTO.IdRecurso = idRecurso;
                    RecursoDTO.Nombre = dbRecurso.Nombre;
                    RecursoDTO.CostoDia = dbRecurso.CostoDia;
                    RecursoDTO.Tipo = dbRecurso.Tipo;
                    RecursoDTO.IdTarea = dbRecurso.IdTarea;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = RecursoDTO;
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
        public async Task<ActionResult> AgregarRecurso(RecursoDTO Recurso)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbRecurso = new Recurso
                {
                    Nombre = Recurso.Nombre,
                    CostoDia = Recurso.CostoDia,
                    Tipo = Recurso.Tipo,
                    IdTarea = Recurso.IdTarea,
                };

                _dbContext.Recursos.Add(dbRecurso);
                await _dbContext.SaveChangesAsync();

                if (dbRecurso.IdRecurso != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbRecurso.IdRecurso;
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
        [Route("{idRecurso}")]
        public async Task<IActionResult> EliminarRecurso(int idRecurso)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbRecurso = await _dbContext.Recursos.FirstOrDefaultAsync(f => f.IdRecurso == idRecurso);

                if (dbRecurso != null)
                {
                    _dbContext.Recursos.Remove(dbRecurso);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Recurso no encontrada";
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
