using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Services.Interfaces
{
    public interface ITallerService
    {
        Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int categoriaId, 
            int? situacion, int page, int rows);

        Task<BaseResponse> AddAsync(TallerDtoRequest request);
    }
}
