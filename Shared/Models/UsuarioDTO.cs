using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.Shared.Models
{
    public class UsuarioDTO
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Dni { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdRol { get; set; }

        public virtual RolDTO? IdRolNavigation { get; set; }

        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();

        public virtual ICollection<UsuarioGrupoDTO> UsuarioGrupos { get; set; } = new List<UsuarioGrupoDTO>();
    }
}
