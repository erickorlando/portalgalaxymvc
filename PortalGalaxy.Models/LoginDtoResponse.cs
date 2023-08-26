using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Models
{
    public class LoginDtoResponse : BaseResponse
    {
        public string NombresCompletos { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
