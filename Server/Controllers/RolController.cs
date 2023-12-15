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
    public class RolController : ControllerBase
    {
        private readonly GestionDeProyectosAdmContext _dbContext;
        private readonly IMapper _mapper;

        public RolController(GestionDeProyectosAdmContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarRols()
        {
            var responseApi = new ResponseAPI<List<RolDTO>>();
            var listaRolsDTO = new List<RolDTO>();
            try
            {
                var rolDb = await _dbContext.Rols.ToListAsync();
                foreach (var rol in rolDb)
                {
                    listaRolsDTO.Add(_mapper.Map<RolDTO>(rol));
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
                    RolDTO = _mapper.Map<RolDTO>(dbRol);

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
        public async Task<ActionResult> AgregarRol(RolDTO rolDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                rolDTO.Usuarios = null;

                var dbRol = _mapper.Map<Rol>(rolDTO);

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

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarRol(int id, RolDTO rolDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbRol = await _dbContext.Rols.Where(c => c.IdRol == id).FirstOrDefaultAsync();

                if (dbRol == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Rol no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbRol with values from rolDTO
                _mapper.Map(rolDTO, dbRol);

                // Clear related collections to avoid unintended updates
                dbRol.Usuarios = null;

                _dbContext.Entry(dbRol).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbRol.IdRol;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar la excepción específica de concurrencia aquí
                // Puedes agregar el código necesario para manejar esta situación
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el rol. No se suministraron las claves primarias correctamente.";
                return StatusCode(500, responseApi);
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
        [Route("{idRol}")]
        public async Task<IActionResult> EliminarRol(int idRol)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuarios = await _dbContext.Usuarios.Where(r => r.IdRol == idRol).ToListAsync();
                if (!dbUsuarios.Any())
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
                        responseApi.Mensaje = "Rol no encontrado";
                    }
                }
                else
                {
                    throw new Exception("Elimine los usuarios del rol previamente");
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
