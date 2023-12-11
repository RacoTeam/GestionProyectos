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
    public class RolController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public RolController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RolDTO>>();
            var listaRolsDTO = new List<RolDTO>();
            try
            {
                foreach (var item in await _dbContext.Rols.Include(d => d.IdRol).ToListAsync())
                {
                    listaRolsDTO.Add(new RolDTO
                    {
                        IdRol = item.IdRol,
                        Nombre = item.Nombre,
                        //TODO
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRolsDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("{idRol}")]
        public async Task<ActionResult> ObtenerRol(int idRol)
        {
            var responseApi = new ResponseAPI<RolDTO>();
            var RolDTO = new RolDTO();

            try
            {
                var dbRol = await _dbContext.Rols.FirstOrDefaultAsync(f => f.IdRol == idRol);

                if (dbRol != null)
                {
                    RolDTO.IdRol = idRol;
                    RolDTO.Nombre = dbRol.Nombre;
                    //TODO

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = RolDTO;
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
        public async Task<ActionResult> AgregarRol(RolDTO Rol)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbRol = new Rol
                {
                    Nombre = Rol.Nombre,
                    //TODO
                };

                _dbContext.Rols.Add(dbRol);
                await _dbContext.SaveChangesAsync();

                if (dbRol.IdRol != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbRol.IdRol;
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
        [Route("{idRol}")]
        public async Task<IActionResult> EliminarRol(int idRol)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbRol = await _dbContext.Rols.FirstOrDefaultAsync(f => f.IdRol == idRol);

                if (dbRol != null)
                {
                    _dbContext.Rols.Remove(dbRol);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Rol no encontrada";
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
