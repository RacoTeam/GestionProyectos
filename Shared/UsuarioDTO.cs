using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string? Usuario1 { get; set; }

        public string? Clave { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public int? Dni { get; set; }

        public int? IdRol { get; set; }

        public virtual RolDTO? IdRolNavigation { get; set; }

        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();

        public virtual ICollection<UsuarioGrupoDTO> UsuarioGrupos { get; set; } = new List<UsuarioGrupoDTO>();
    }
}
