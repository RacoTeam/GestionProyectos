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
        [Route("{idUsuario}")]
        public async Task<ActionResult> ObtenerUsuarioGrupoTarea(int idUsuario)
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoTareaDTO>>();
            var listaUsuarioGruposTareaDTO = new List<UsuarioGrupoTareaDTO>();

            try
            {
                var dbUsuarioGrupoTarea = await _dbContext.UsuarioGrupoTareas.Where(f => f.IdUsuario == idUsuario).ToListAsync();

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

        [HttpPost]
        public async Task<ActionResult> AgregarUsuarioGrupoTarea(UsuarioGrupoTareaDTO usuarioGrupoTareaDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbUsuarioGrupoTarea = _mapper.Map<UsuarioGrupoTarea>(usuarioGrupoTareaDTO);

                _dbContext.UsuarioGrupoTareas.Add(dbUsuarioGrupoTarea);
                await _dbContext.SaveChangesAsync();

                if (dbUsuarioGrupoTarea.IdGrupo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = 0;
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
        public async Task<IActionResult> EliminarUsuarioGrupoTarea(int idUsuario, int idTarea, int idGrupo, int idProyecto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuarioGrupoTarea = await _dbContext.UsuarioGrupoTareas.FirstOrDefaultAsync(
                    f => f.IdUsuario == idUsuario && f.IdGrupo == idGrupo && f.IdTarea == idTarea && f.IdProyecto == idProyecto);

                if (dbUsuarioGrupoTarea != null)
                {
                    _dbContext.UsuarioGrupoTareas.Remove(dbUsuarioGrupoTarea);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado";
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
