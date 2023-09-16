using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Models.Request;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.ApiRest.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TalleresController : ControllerBase
    {
        private readonly ITallerService _service;

        public TalleresController(ITallerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
        {
            var response = await _service.ListAsync(request.Filter, request.CategoriaId, request.Situacion, request.Page, request.Rows);

            return Ok(response);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var response = await _service.GetAsync(id);

        //    return response.Success ? Ok(response) : NotFound(response);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(TallerDtoRequest request)
        {
            var response = await _service.AddAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, Request request)
        //{
        //    var response = await _service.UpdateAsync(id, request);

        //    return response.Success ? Ok(response) : NotFound(response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _service.DeleteAsync(id);

        //    return response.Success ? Ok(response) : NotFound(response);
        //}
    }
}
