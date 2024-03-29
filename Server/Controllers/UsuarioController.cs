﻿using AutoMapper;
using GestionProyectos.Server.Data;
using GestionProyectos.Server.Models;
using GestionProyectos.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public UsuarioController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult> ListarUsuarios()
        {
            var responseApi = new ResponseAPI<List<UsuarioDTO>>();
            var listaUsuariosDTO = new List<UsuarioDTO>();
            try
            {
                var usuariosDb = await _dbContext.Usuarios.ToListAsync();
                foreach (var usuario in usuariosDb)
                {
                    usuario.IdRolNavigation = await _dbContext.Rols.FirstOrDefaultAsync(r => r.IdRol == usuario.IdRol);

                    usuario.Proyectos = await _dbContext.Proyectos.Where(p => p.IdUsuario == usuario.IdUsuario).ToListAsync();
                    foreach (var proyecto in usuario.Proyectos)
                    {
                        proyecto.Grupos = null;
                        proyecto.Tareas = null;
                        proyecto.IdClienteNavigation = null;
                        proyecto.IdUsuarioNavigation = null;
                    }

                    usuario.UsuarioGrupos = await _dbContext.UsuarioGrupos.Where(g => g.IdUsuario == usuario.IdUsuario).ToListAsync();
                    foreach (var usuariogrupo in usuario.UsuarioGrupos)
                    {
                        usuariogrupo.UsuarioGrupoTareas = null;
                        usuariogrupo.IdUsuarioNavigation = null;
                    }

                    listaUsuariosDTO.Add(_mapper.Map<UsuarioDTO>(usuario));
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
            var usuarioDTO = new UsuarioDTO();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario);

                if (dbUsuario != null)
                {
                    dbUsuario.IdRolNavigation = await _dbContext.Rols.FirstOrDefaultAsync(r => r.IdRol == dbUsuario.IdRol);

                    dbUsuario.Proyectos = await _dbContext.Proyectos.Where(p => p.IdUsuario == dbUsuario.IdUsuario).ToListAsync();
                    foreach (var proyecto in dbUsuario.Proyectos)
                    {
                        proyecto.Grupos = null;
                        proyecto.Tareas = null;
                        proyecto.IdClienteNavigation = null;
                        proyecto.IdUsuarioNavigation = null;
                    }

                    dbUsuario.UsuarioGrupos = await _dbContext.UsuarioGrupos.Where(g => g.IdUsuario == dbUsuario.IdUsuario).ToListAsync();
                    foreach (var usuariogrupo in dbUsuario.UsuarioGrupos)
                    {
                        usuariogrupo.UsuarioGrupoTareas = null;
                        usuariogrupo.IdUsuarioNavigation = null;
                    }

                    usuarioDTO = _mapper.Map<UsuarioDTO>(dbUsuario);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = usuarioDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontró el usuario";
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
        public async Task<ActionResult> AgregarUsuario(UsuarioDTO usuarioDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                usuarioDTO.IdRolNavigation = null;
                usuarioDTO.UsuarioGrupos = null;
                usuarioDTO.Proyectos = null;

                var dbUsuario = _mapper.Map<Usuario>(usuarioDTO);

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

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarUsuario(int id, UsuarioDTO usuarioDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .Include(u => u.Proyectos)
                    .Include(u => u.UsuarioGrupos)
                    .Where(u => u.IdUsuario == id)
                    .FirstOrDefaultAsync();

                if (dbUsuario == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbUsuario with values from usuarioDTO
                _mapper.Map(usuarioDTO, dbUsuario);

                // Clear related collections to avoid unintended updates
                dbUsuario.IdRolNavigation = null;
                dbUsuario.Proyectos = null;
                dbUsuario.UsuarioGrupos = null;

                _dbContext.Entry(dbUsuario).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbUsuario.IdUsuario;
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
        [Route("{idUsuario}")]
        public async Task<IActionResult> EliminarUsuario(int idUsuario)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(f => f.IdUsuario == idUsuario);

                if (dbUsuario != null)
                {
                    _dbContext.Usuarios.Remove(dbUsuario);
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

        [HttpGet]
        public async Task<IActionResult> Buscar(string nombreUsuario, string Clave)
        {
            var responseApi = new ResponseAPI<UsuarioDTO>();
            var usuarioDTO = new UsuarioDTO();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(f => f.NombreUsuario == nombreUsuario && f.Clave == Clave);

                if (dbUsuario != null)
                {
                    dbUsuario.IdRolNavigation = await _dbContext.Rols.FirstOrDefaultAsync(r => r.IdRol == dbUsuario.IdRol);

                    dbUsuario.Proyectos = await _dbContext.Proyectos.Where(p => p.IdUsuario == dbUsuario.IdUsuario).ToListAsync();
                    foreach (var proyecto in dbUsuario.Proyectos)
                    {
                        proyecto.Grupos = null;
                        proyecto.Tareas = null;
                        proyecto.IdClienteNavigation = null;
                        proyecto.IdUsuarioNavigation = null;
                    }

                    dbUsuario.UsuarioGrupos = await _dbContext.UsuarioGrupos.Where(g => g.IdUsuario == dbUsuario.IdUsuario).ToListAsync();
                    foreach (var usuariogrupo in dbUsuario.UsuarioGrupos)
                    {
                        usuariogrupo.UsuarioGrupoTareas = null;
                        usuariogrupo.IdUsuarioNavigation = null;
                    }

                    usuarioDTO = _mapper.Map<UsuarioDTO>(dbUsuario);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = usuarioDTO;
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
    }

}
