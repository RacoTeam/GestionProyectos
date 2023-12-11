using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class UsuarioGrupoDTO
    {
        public int IdUsuario { get; set; }

        public int IdGrupo { get; set; }

        public int IdProyecto { get; set; }

        public virtual GrupoDTO Id { get; set; } = null!;

        public virtual UsuarioDTO IdUsuarioNavigation { get; set; } = null!;

        public virtual ICollection<UsuarioGrupoTareaDTO> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTareaDTO>();
    }
}
