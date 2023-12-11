using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    class RecursoDTO
    {
        public int IdRecurso { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? CostoDia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Tipo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdTarea { get; set; }

        public virtual TareaDTO? IdTareaNavigation { get; set; }
    }
}
