using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    public class UsuarioGrupoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdGrupo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProyecto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public virtual GrupoDTO Grupo { get; set; } = null!;

        public virtual UsuarioDTO IdUsuarioNavigation { get; set; } = null!;

        public virtual ICollection<UsuarioGrupoTareaDTO> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
    }
}
