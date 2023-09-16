using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.WebMvc.Services.Interfaces;

public interface ITallerProxy : ICrudRestHelper<TallerDtoRequest, TallerDtoResponse>
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request);
}