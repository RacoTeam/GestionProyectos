using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.Shared.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ingrese usuario")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese contraseña")]
        public string? Clave { get; set; }
    }
}
