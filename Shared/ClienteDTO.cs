using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared
{
    class ClienteDTO
    {
        public int IdCliente { get; set; }

        public string? Nombre { get; set; }

        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();
    }
}
