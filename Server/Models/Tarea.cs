using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public double? Avance { get; set; }

    public int? IdProyecto { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

    public virtual ICollection<UsuarioGrupoTarea> UsuarioGrupoTareas { get; set; } = new List<UsuarioGrupoTarea>();
}
