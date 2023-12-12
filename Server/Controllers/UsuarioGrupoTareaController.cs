using AutoMapper;
using GestionProyectos.Server.Data;
using GestionProyectos.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioGrupoTareaController : Controller
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public UsuarioGrupoTareaController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarUsuariosGruposTarea()
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoTareaDTO>>();
            var listaUsuarioGruposTareaDTO = new List<UsuarioGrupoTareaDTO>();
            try
            {
                var usuariosGruposTareaDb = await _dbContext.UsuarioGrupoTareas.ToListAsync();
                foreach (var usuarioG in usuariosGruposTareaDb)
                {
                    listaUsuarioGruposTareaDTO.Add(_mapper.Map<UsuarioGrupoTareaDTO>(usuarioG));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioGruposTareaDTO;
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
        public async Task<ActionResult> ObtenerUsuarioGrupoTarea(int idUsuarioGrupo)
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoTareaDTO>>();
            var listaUsuarioGruposTareaDTO = new List<UsuarioGrupoTareaDTO>();

            try
            {
                var dbUsuarioGrupoTarea = await _dbContext.UsuarioGrupoTareas.Where(f => f.IdUsuario == idUsuarioGrupo).ToListAsync();

                foreach (var usuarioG in dbUsuarioGrupoTarea)
                {
                    listaUsuarioGruposTareaDTO.Add(_mapper.Map<UsuarioGrupoTareaDTO>(usuarioG));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioGruposTareaDTO;


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
