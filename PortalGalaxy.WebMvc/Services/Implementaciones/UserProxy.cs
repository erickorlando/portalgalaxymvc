using PortalGalaxy.Models;
using PortalGalaxy.Models.Response;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Services.Implementaciones
{
    public class UserProxy : RestBase, IUserProxy
    {
        public UserProxy(HttpClient httpClient) 
            : base("api/Users", httpClient)
        {

        }

        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            return await SendAsync<LoginDtoRequest, LoginDtoResponse>(request, HttpMethod.Post, "Login");
        }

        public async Task<BaseResponse> RegisterAsync(RegisterDtoRequest request)
        {
            return await SendAsync<RegisterDtoRequest, BaseResponse>(request, HttpMethod.Post, "Register");
        }
    }
}
