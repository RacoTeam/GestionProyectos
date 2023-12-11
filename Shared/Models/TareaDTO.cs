using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    class TareaDTO
    {
        public int IdTarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? FechaInicio { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? FechaFin { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? Avance { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdProyecto { get; set; }

        public virtual ProyectoDTO? IdProyectoNavigation { get; set; }

        public virtual ICollection<RecursoDTO> Recursos { get; set; } = new List<RecursoDTO>();

        public virtual ICollection<UsuarioGrupoTareaDTO> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
    }
}
