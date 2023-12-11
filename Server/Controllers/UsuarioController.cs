using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using GestionProyectos.Server.Models;
using GestionProyectos.Server.Data;
using GestionProyectos.Shared.Models;

//using Microsoft.EntityFrameworkCore;


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
            //var responseApi = new ResponseAPI<List<UsuarioDTO>>();
            //var listaUsuarios = new List<UsuarioDTO>();    


        }
    }
}
