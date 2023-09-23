using AutoMapper;
using PortalGalaxy.Entities;
using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Services.Profiles
{
    public class PortalGalaxyProfile : Profile
    {
        public PortalGalaxyProfile()
        {
            // El AutoMapper solo mapea de izquierda a derecha

            // Categorias
            CreateMap<Categoria, CategoriaDtoResponse>();


            // Alumnos

            CreateMap<Alumno, AlumnoDtoResponse>();

            CreateMap<AlumnoDtoRequest, Alumno>();

            CreateMap<Taller, TallerDtoResponse>()
                .ForMember(d => d.Fecha, o => o.MapFrom(x => $"{x.FechaInicio:dd/MM/yyyy} {x.HoraInicio:hh:mm tt}"));

            CreateMap<TallerDtoRequest, Taller>();

            CreateMap<InstructorInfo, InstructorDtoResponse>();

            CreateMap<InstructorDtoRequest, Instructor>();
        }
    }
}
