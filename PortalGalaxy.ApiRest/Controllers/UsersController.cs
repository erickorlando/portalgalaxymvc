using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Models;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.ApiRest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDtoRequest request)
        {
            var response = await _service.LoginAsync(request);

            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDtoRequest request)
        {
            var response = await _service.RegisterAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
