using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface IInstructorService
{
    Task<BaseResponseGeneric<ICollection<InstructorDtoResponse>>> ListAsync(string? nombre, string? nroDocumento,
        int? categoriaId);

    Task<BaseResponseGeneric<InstructorDtoResponse>> FindByIdAsync(int id);

    Task<BaseResponse> AddAsync(InstructorDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, InstructorDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}