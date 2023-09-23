using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Models.Request;
using PortalGalaxy.ViewModels;
using PortalGalaxy.ViewModels.Exceptions;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Controllers;

public class InstructorController : Controller
{
    private readonly IInstructorProxy _proxy;
    private readonly ICategoriaProxy _categoriaProxy;
    private readonly ILogger<InstructorController> _logger;

    public InstructorController(IInstructorProxy proxy, ICategoriaProxy categoriaProxy, ILogger<InstructorController> logger)
    {
        _proxy = proxy;
        _categoriaProxy = categoriaProxy;
        _logger = logger;
    }

    // GET
    public async Task<IActionResult> BusquedaInstructor(BusquedaInstructorViewModel model)
    {
        try
        {
            model.Categorias = await _categoriaProxy.ListAsync();

            return PartialView($"_{nameof(BusquedaInstructor)}", model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", ex.Message);
            return PartialView($"_{nameof(BusquedaInstructor)}", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Resultados(BusquedaInstructorViewModel model)
    {
        try
        {
            var response = await _proxy.ListAsync(model.Nombres, model.NroDocumento, model.CategoriaSeleccionada);

            model.Instructores = response;

            return PartialView("_ResultadosBusquedaInstructor", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Busqueda de Instructores {Message}", ex.Message);
            return PartialView("_ResultadosBusquedaInstructor", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(BusquedaInstructorViewModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Nombres))
            {
                throw new ModelException(nameof(model.Nombres), "El nombre esta vacio");
            }

            if (string.IsNullOrWhiteSpace(model.NroDocumento))
            {
                throw new ModelException(nameof(model.NroDocumento), "El Nro. de Documento, no puede estar vacio");
            }

            if (model.CategoriaSeleccionada is null)
            {
                throw new ModelException(nameof(model.CategoriaSeleccionada), "Seleccione una Categoria");
            }

            await _proxy.CreateAsync(new InstructorDtoRequest
            {
                Nombres = model.Nombres,
                NroDocumento = model.NroDocumento,
                CategoriaId = model.CategoriaSeleccionada.Value
            });

            return PartialView("_ResultadosBusquedaInstructor", model);
        }
        catch (ModelException ex)
        {
            ModelState.AddModelError(ex.PropertyName, ex.Message);
            _logger.LogError(ex, "Validacion de Registro {Message}", ex.Message);
            return PartialView("_ResultadosBusquedaInstructor", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registro de Instructores {Message}", ex.Message);
            return PartialView("_ResultadosBusquedaInstructor", model);
        }
    }
}