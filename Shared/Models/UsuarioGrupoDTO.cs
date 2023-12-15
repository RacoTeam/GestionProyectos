using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual GrupoDTO? Grupo { get; set; } = null!;
    
        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UsuarioGrupoTareaDTO>? UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
    }
}