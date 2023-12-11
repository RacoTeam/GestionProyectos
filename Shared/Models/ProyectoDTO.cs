using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    class ProyectoDTO
    {
        public int IdProyecto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
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
