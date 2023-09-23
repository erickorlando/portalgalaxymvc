using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.WebMvc.Services.Interfaces;

public interface IInstructorProxy : ICrudRestHelper<InstructorDtoRequest, InstructorDtoResponse>
{
    Task<ICollection<InstructorDtoResponse>> ListAsync(string? nombre, string? nroDocumento, int? categoriaId);
}