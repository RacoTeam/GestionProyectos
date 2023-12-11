using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Usuario1 { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Clave { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? Dni { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdRol { get; set; }

        public virtual RolDTO? IdRolNavigation { get; set; }

        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();

        public virtual ICollection<UsuarioGrupoDTO> UsuarioGrupos { get; set; } = new List<UsuarioGrupoDTO>();
    }
}
