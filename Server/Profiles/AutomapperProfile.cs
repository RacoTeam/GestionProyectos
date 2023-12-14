using AutoMapper;
using GestionProyectos.Server.Models;
using GestionProyectos.Shared.Models;

namespace GestionProyectos.Server.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Proyecto, ProyectoDTO>().ReverseMap();
            CreateMap<Recurso, RecursoDTO>().ReverseMap();
            CreateMap<Tarea, TareaDTO>().ReverseMap();
            CreateMap<Grupo, GrupoDTO>().ReverseMap();
            CreateMap<UsuarioGrupo, UsuarioGrupoDTO>().ReverseMap();
            CreateMap<UsuarioGrupoTarea, UsuarioGrupoTareaDTO>().ReverseMap();
        }
    }
}
