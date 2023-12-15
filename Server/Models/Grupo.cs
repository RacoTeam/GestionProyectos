using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public int IdProyecto { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaEliminacion { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get; set; } = new List<UsuarioGrupo>();
}
