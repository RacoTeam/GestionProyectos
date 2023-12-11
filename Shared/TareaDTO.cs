using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class TareaDTO
    {
        public int IdTarea { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public double? Avance { get; set; }

        public int? IdProyecto { get; set; }

        public virtual ProyectoDTO? IdProyectoNavigation { get; set; }

        public virtual ICollection<RecursoDTO> Recursos { get; set; } = new List<RecursoDTO>();

        public virtual ICollection<UsuarioGrupoTareaDTO> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
    }
}
