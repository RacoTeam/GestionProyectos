using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class UsuarioGrupo
{
    public int IdUsuario { get; set; }

    public int IdGrupo { get; set; }

    public int IdProyecto { get; set; }

    public virtual Grupo Grupo { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioGrupoTarea> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTarea>();
}
