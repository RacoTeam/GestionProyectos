using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class GrupoDTO
    {
        public int IdGrupo { get; set; }

        public int IdProyecto { get; set; }

        public string? Nombre { get; set; }

        public virtual ProyectoDTO IdProyectoNavigation { get; set; } = null!;

        public virtual ICollection<UsuarioGrupoDTO> UsuarioGrupos { get; set; } = new List<UsuarioGrupoDTO>();
    }
}
