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

    public class GrupoController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public GrupoController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<GrupoDTO>>();
            var listaGruposDTO = new List<GrupoDTO>();
            try
            {
                foreach (var item in await _dbContext.Grupos.Include(d => d.IdGrupo).ToListAsync())
                {
                    listaGruposDTO.Add(new GrupoDTO
                    {
                        IdGrupo = item.IdGrupo,
                        IdProyecto = item.IdProyecto,
                        Nombre = item.Nombre,
                        //TODO
                    });
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
                var dbGrupo = await _dbContext.Grupos.FirstOrDefaultAsync(f => f.IdGrupo == idGrupo);

                if (dbGrupo != null)
                {
                    GrupoDTO.IdGrupo = idGrupo;
                    GrupoDTO.IdProyecto = dbGrupo.IdProyecto;
                    GrupoDTO.Nombre = dbGrupo.Nombre;
                    //TODO

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
