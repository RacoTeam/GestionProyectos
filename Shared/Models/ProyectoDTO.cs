using System.ComponentModel.DataAnnotations;
using CG.Blazor.Forms.Attributes;

namespace GestionProyectos.Shared.Models
{
    [RenderValidationSummary()]
    [RenderFluentValidationValidator]
    public class ProyectoDTO
    {
        [Key]
        public int IdProyecto { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [RenderMudTextField()]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 255)]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? FechaInicio { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? FechaFin { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdCliente { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdUsuario { get; set; } //Responsable
        public virtual ICollection<GrupoDTO> Grupos { get; set; } = new List<GrupoDTO>();

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; }

        public virtual ICollection<TareaDTO> Tareas { get; set; } = new List<TareaDTO>();
    }
}
