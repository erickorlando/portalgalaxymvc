using PortalGalaxy.Models;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

        Task<BaseResponse> RegisterAsync(RegisterDtoRequest request);
    }
}
