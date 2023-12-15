using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaEliminacion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
