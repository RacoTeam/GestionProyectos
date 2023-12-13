using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    public class GrupoDTO
    {
        [Key]
        public int IdGrupo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProyecto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }

        [JsonIgnore]
        public virtual ProyectoDTO? IdProyectoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UsuarioGrupoDTO>? UsuarioGrupos { get; set; } = new List<UsuarioGrupoDTO>();
    }
}
