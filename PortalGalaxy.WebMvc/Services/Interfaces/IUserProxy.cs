using PortalGalaxy.Models;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.WebMvc.Services.Interfaces
{
    public interface IUserProxy
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

        Task<BaseResponse> RegisterAsync(RegisterDtoRequest request);
    }
}
