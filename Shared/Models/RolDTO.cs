using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    class RolDTO
    {
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        public virtual ICollection<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();
    }
}
