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
    public class UsuarioController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;


        public UsuarioController(GestionDeProyectosAdmContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioDTO>>();
            var listaUsuariosDTO = new List<UsuarioDTO>(); 
            try
            {
                foreach (var item in await _dbContext.Usuarios.Include(d => d.IdUsuario).ToListAsync())
                {
                    listaUsuariosDTO.Add(new UsuarioDTO
                    {
                        IdUsuario = item.IdUsuario,
                        Usuario1 = item.Usuario1,
                        Clave = item.Clave,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Dni = item.Dni,
                        IdRol = item.IdRol,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuariosDTO;
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
        public async Task<ActionResult> ObtenerUsuario(int idUsuario)
        {
            var responseApi = new ResponseAPI<UsuarioDTO>();
            var UsuarioDTO = new UsuarioDTO();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario);

                if (dbUsuario != null)
                {
                    UsuarioDTO.IdUsuario = idUsuario;

                    UsuarioDTO.Usuario1 = dbUsuario.Usuario1;
                    UsuarioDTO.Clave = dbUsuario.Clave;
                    UsuarioDTO.Nombre = dbUsuario.Nombre;
                    UsuarioDTO.Apellido = dbUsuario.Apellido;
                    UsuarioDTO.Dni = dbUsuario.Dni;
                    UsuarioDTO.IdRol = dbUsuario.IdRol;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = UsuarioDTO;
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
        public async Task<ActionResult> AgregarUsuario(UsuarioDTO Usuario)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbUsuario = new Usuario
                {
                    Usuario1 = Usuario.Usuario1,
                    Clave = Usuario.Clave,
                    Nombre = Usuario.Nombre,
                    Apellido = Usuario.Apellido,
                    Dni = Usuario.Dni,
                    IdRol = Usuario.IdRol,
            };

                _dbContext.Usuarios.Add(dbUsuario);
                await _dbContext.SaveChangesAsync();

                if (dbUsuario.IdUsuario != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUsuario.IdUsuario;
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
        [Route("{idUsuario}")]
        public async Task<IActionResult> EliminarUsuario(int idUsuario)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(f => f.Id == idUsuario);

                if (dbUsuario != null)
                {
                    _dbContext.Usuarios.Remove(dbUsuario);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrada";
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
