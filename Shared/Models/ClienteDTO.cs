using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionProyectos.Shared.Models
{
    public class ClienteDTO
    {
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();
    }
}