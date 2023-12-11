using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class UsuarioGrupoTareaDTO
    {
        public int IdUsuario { get; set; }

        public int IdGrupo { get; set; }

        public int IdTarea { get; set; }

        public int IdProyecto { get; set; }

        public virtual TareaDTO Id { get; set; } = null!;

        public virtual UsuarioGrupoDTO IdNavigation { get; set; } = null!;
    }
}
