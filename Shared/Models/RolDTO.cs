using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionProyectos.Shared.Models
{
    public class RolDTO
    {
        [Key]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();
    }
}
