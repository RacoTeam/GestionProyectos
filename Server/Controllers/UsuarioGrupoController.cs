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
    public class UsuarioGrupoController : Controller
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public UsuarioGrupoController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
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
                foreach (var usuarioG in usuariosGruposDb)
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
        [Route("{idUsuario}")]
        public async Task<ActionResult> ObtenerUsuarioGrupo(int idUsuario)
        {
            var responseApi = new ResponseAPI<List<UsuarioGrupoDTO>>();
            var listaUsuarioGruposDTO = new List<UsuarioGrupoDTO>();

            try
            {
                var dbUsuarioGrupo = await _dbContext.UsuarioGrupos.Where(f => f.IdUsuario == idUsuario).ToListAsync();

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

        [HttpPost]
        public async Task<ActionResult> AgregarUsuarioGrupo(UsuarioGrupoDTO usuarioGrupoDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbUsuarioGrupo = _mapper.Map<UsuarioGrupo>(usuarioGrupoDTO);

                _dbContext.UsuarioGrupos.Add(dbUsuarioGrupo);
                await _dbContext.SaveChangesAsync();

                if (dbUsuarioGrupo.IdGrupo != 0)
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

        [HttpPut]
        public async Task<ActionResult> ModificarUsuarioGrupo(int idUsuario, int idGrupo, int idProyecto, UsuarioGrupoDTO usuarioGrupoDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Buscar y eliminar la entidad existente
                var dbUsuarioGrupo = await _dbContext.UsuarioGrupos
                    .Where(ug => ug.IdUsuario == idUsuario && ug.IdGrupo == idGrupo && ug.IdProyecto == idProyecto)
                    .FirstOrDefaultAsync();

                if (dbUsuarioGrupo == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "UsuarioGrupo no encontrado";
                    return NotFound(responseApi);
                }

                _dbContext.UsuarioGrupos.Remove(dbUsuarioGrupo);
                await _dbContext.SaveChangesAsync();

                // Agregar una nueva entidad con los valores actualizados
                var nuevaUsuarioGrupo = new UsuarioGrupo
                {
                    IdUsuario = usuarioGrupoDTO.IdUsuario,
                    IdGrupo = usuarioGrupoDTO.IdGrupo,
                    IdProyecto = usuarioGrupoDTO.IdProyecto
                    // Agrega las propiedades adicionales que necesitas actualizar
                };

                _dbContext.UsuarioGrupos.Add(nuevaUsuarioGrupo);
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = nuevaUsuarioGrupo.IdUsuario; // O cualquier propiedad que desees devolver
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi); // Internal Server Error
            }

            return Ok(responseApi);
        }



        [HttpDelete]
        public async Task<IActionResult> EliminarUsuarioGrupo(int idUsuario, int idGrupo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuarioGrupo = await _dbContext.UsuarioGrupos.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.IdGrupo == idGrupo);

                if (dbUsuarioGrupo != null)
                {
                    _dbContext.UsuarioGrupos.Remove(dbUsuarioGrupo);
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
