using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class RolDTO
    {
        public int IdRol { get; set; }

        public string? Nombre { get; set; }

        public virtual ICollection<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();
    }
}
