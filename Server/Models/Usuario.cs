using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Clave { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Dni { get; set; }

    public int? IdRol { get; set; }

    public DateTime? FechaEliminacion { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get; set; } = new List<UsuarioGrupo>();
}
