using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.Shared.Models
{
    public class RecursoDTO
    {
        [Key]
        public int IdRecurso { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double? CostoDia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Tipo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdTarea { get; set; }

        public virtual TareaDTO? IdTareaNavigation { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
