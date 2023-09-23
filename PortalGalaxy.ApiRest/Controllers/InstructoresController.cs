using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Models.Request;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.ApiRest.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InstructoresController : ControllerBase
{
    private readonly IInstructorService _service;

    public InstructoresController(IInstructorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? nombre, string? nroDocumento, int? categoriaId)
    {
        var response = await _service.ListAsync(nombre, nroDocumento, categoriaId);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InstructorDtoRequest request)
    {
        var response = await _service.AddAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, InstructorDtoRequest request)
    {
        var response = await _service.UpdateAsync(id, request);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }
}