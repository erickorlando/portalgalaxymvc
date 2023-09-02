using AutoMapper;
using PortalGalaxy.Entities;
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
        }
    }
}
