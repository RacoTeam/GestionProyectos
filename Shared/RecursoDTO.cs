using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class RecursoDTO
    {
        public int IdRecurso { get; set; }

        public string? Nombre { get; set; }

        public double? CostoDia { get; set; }

        public string? Tipo { get; set; }

        public int? IdTarea { get; set; }

        public virtual TareaDTO? IdTareaNavigation { get; set; }
    }
}
