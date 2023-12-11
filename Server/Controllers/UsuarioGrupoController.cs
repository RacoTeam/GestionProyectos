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
    public class UsuarioGrupoController : Controller
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public UsuarioGrupoController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoDTO>>();
            var listaUsuarioGruposDTO = new List<UsuarioGrupoDTO>();
            try
            {
                //foreach (var item in await _dbContext.UsuarioGrupos.Include(d => d.IdUsuarioGrupo).ToListAsync())
                //{
                //    listaUsuarioGruposDTO.Add(new UsuarioGrupoDTO
                //    {
                //        IdUsuarioGrupo = item.IdUsuarioGrupo,
                //        UsuarioGrupo1 = item.UsuarioGrupo1,
                //        Clave = item.Clave,
                //        Nombre = item.Nombre,
                //        Apellido = item.Apellido,
                //        Dni = item.Dni,
                //        IdRol = item.IdRol,
                //        //TODO
                //    });
                //}

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioGruposDTO;
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
