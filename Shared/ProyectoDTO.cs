using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class ProyectoDTO
    {
        public int IdProyecto { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int? IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public virtual ICollection<GrupoDTO> Grupos { get; set; } = new List<GrupoDTO>();

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; }

        public virtual ICollection<TareaDTO> Tareas { get; set; } = new List<TareaDTO>();
    }
}
