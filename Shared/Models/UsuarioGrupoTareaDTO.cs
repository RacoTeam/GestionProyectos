using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual TareaDTO? Id { get; set; } = null!;

        [JsonIgnore]
        public virtual UsuarioGrupoDTO? IdNavigation { get; set; } = null!;
    }
}
