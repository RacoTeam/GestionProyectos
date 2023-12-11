using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    public class UsuarioGrupoTareaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdGrupo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdTarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProyecto { get; set; }

        public virtual TareaDTO Id { get; set; } = null!;

        public virtual UsuarioGrupoDTO IdNavigation { get; set; } = null!;
    }
}
