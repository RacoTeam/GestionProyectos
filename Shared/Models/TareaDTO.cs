using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionProyectos.Shared.Models
{
    public class TareaDTO
    {
        [Key]
        public int IdTarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }
        [StringLength(maximumLength: 255)]
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public double? Avance { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdProyecto { get; set; }
        [JsonIgnore]
        public virtual ProyectoDTO? IdProyectoNavigation { get; set; }
        public virtual ICollection<RecursoDTO>? Recursos { get; set; } = new List<RecursoDTO>();
        [JsonIgnore]
        public virtual ICollection<UsuarioGrupoTareaDTO>? UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
        public DateTime? FechaEliminacion { get; set; }
    }
}
