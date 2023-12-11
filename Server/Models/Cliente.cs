using System;
using System.Collections.Generic;
using GestionProyectos.Server.Models;

namespace GestionProyectos.Server;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
