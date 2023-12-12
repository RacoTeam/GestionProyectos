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
    public class UsuarioGrupoController : Controller
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public UsuarioGrupoController(GestionDeProyectosAdmContext dbcontext,IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarUsuariosGrupos()
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoDTO>>();
            var listaUsuarioGruposDTO = new List<UsuarioGrupoDTO>();
            try
            {
                var usuariosGruposDb = await _dbContext.UsuarioGrupos.ToListAsync();
                foreach(var usuarioG in usuariosGruposDb)
                {
                    usuarioG.IdUsuarioNavigation = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuarioG.IdUsuario);
                    usuarioG.IdUsuarioNavigation.UsuarioGrupos = null;

                    listaUsuarioGruposDTO.Add(_mapper.Map<UsuarioGrupoDTO>(usuarioG));
                }

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

        [HttpGet]
        [Route("{idUsuarioGrupo}")]
        public async Task<ActionResult> ObtenerUsuarioGrupo(int idUsuarioGrupo)
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoDTO>>();
            var listaUsuarioGruposDTO = new List<UsuarioGrupoDTO>();

            try
            {
                var dbUsuarioGrupo = await _dbContext.UsuarioGrupos.Where(f => f.IdUsuario == idUsuarioGrupo).ToListAsync();

                foreach (var usuarioG in dbUsuarioGrupo)
                {
                    usuarioG.IdUsuarioNavigation = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuarioG.IdUsuario);
                    usuarioG.IdUsuarioNavigation.UsuarioGrupos = null;

                    listaUsuarioGruposDTO.Add(_mapper.Map<UsuarioGrupoDTO>(usuarioG));
                }

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
