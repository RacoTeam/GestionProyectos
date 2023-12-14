namespace GestionProyectos.Server.Models;

public partial class UsuarioGrupoTarea
{
    public int IdUsuario { get; set; }

    public int IdGrupo { get; set; }

    public int IdTarea { get; set; }

    public int IdProyecto { get; set; }

    public virtual Tarea Id { get; set; } = null!;

    public virtual UsuarioGrupo IdNavigation { get; set; } = null!;
}
