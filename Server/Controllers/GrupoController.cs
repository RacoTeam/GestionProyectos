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

    public class GrupoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public GrupoController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarGrupos()
        {
            var responseApi = new ResponseAPI<List<GrupoDTO>>();
            var listaGruposDTO = new List<GrupoDTO>();
            try
            {
                var gruposDb = await _dbContext.Grupos.ToListAsync();
                foreach (var grupo in gruposDb)
                {
                    grupo.IdProyectoNavigation = await _dbContext.Proyectos.FirstOrDefaultAsync(g => g.IdProyecto == grupo.IdProyecto);

                    grupo.IdProyectoNavigation.Grupos = null;

                    listaGruposDTO.Add(_mapper.Map<GrupoDTO>(grupo));

                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaGruposDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("{idGrupo}")]
        public async Task<ActionResult> ObtenerGrupo(int idGrupo)
        {
            var responseApi = new ResponseAPI<GrupoDTO>();
            var GrupoDTO = new GrupoDTO();

            try
            {
                var dbGrupo = await _dbContext.Grupos.FirstOrDefaultAsync(g => g.IdGrupo == idGrupo);
                if (dbGrupo != null)
                {

                    dbGrupo.IdProyectoNavigation = await _dbContext.Proyectos.FirstOrDefaultAsync(p => p.IdProyecto == dbGrupo.IdProyecto);

                    dbGrupo.IdProyectoNavigation.Grupos = null;

                    GrupoDTO = _mapper.Map<GrupoDTO>(dbGrupo);


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = GrupoDTO;
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
        public async Task<ActionResult> AgregarGrupo(GrupoDTO Grupo)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbGrupo = new Grupo
                {
                    IdGrupo = Grupo.IdGrupo,
                    IdProyecto = Grupo.IdProyecto,
                    Nombre = Grupo.Nombre,
                    //TODO
                };

                _dbContext.Grupos.Add(dbGrupo);
                await _dbContext.SaveChangesAsync();

                if (dbGrupo.IdGrupo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbGrupo.IdGrupo;
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
        [Route("{idGrupo}")]
        public async Task<IActionResult> EliminarGrupo(int idGrupo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbGrupo = await _dbContext.Grupos.FirstOrDefaultAsync(f => f.IdGrupo == idGrupo);

                if (dbGrupo != null)
                {
                    _dbContext.Grupos.Remove(dbGrupo);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Grupo no encontrada";
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
