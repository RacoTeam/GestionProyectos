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
    public class RecursoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public RecursoController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarRecursos()
        {
            var responseApi = new ResponseAPI<List<RecursoDTO>>();
            var listaRecursosDTO = new List<RecursoDTO>();
            try
            {
                var recursosDb = await _dbContext.Recursos.ToListAsync();
                foreach (var recurso in recursosDb)
                {

                    recurso.IdTareaNavigation = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.IdTarea == recurso.IdTarea);
                    recurso.IdTareaNavigation.Recursos = null;

                    listaRecursosDTO.Add(_mapper.Map<RecursoDTO>(recurso));
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

                    dbRecurso.IdTareaNavigation = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.IdTarea == dbRecurso.IdTarea);
                    dbRecurso.IdTareaNavigation.Recursos = null;


                    RecursoDTO = _mapper.Map<RecursoDTO>(dbRecurso);

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
        public async Task<ActionResult> AgregarRecurso(RecursoDTO recursoDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                recursoDTO.IdTareaNavigation = null;

                var dbRecurso = _mapper.Map<Recurso>(recursoDTO);

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
