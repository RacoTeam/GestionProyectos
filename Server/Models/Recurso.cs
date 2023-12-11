using System;
using System.Collections.Generic;

namespace GestionProyectos.Server.Models;

public partial class Recurso
{
    public int IdRecurso { get; set; }

    public string? Nombre { get; set; }

    public double? CostoDia { get; set; }

    public string? Tipo { get; set; }

    public int? IdTarea { get; set; }

    public virtual Tarea? IdTareaNavigation { get; set; }
}
