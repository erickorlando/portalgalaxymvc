using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Models;
using PortalGalaxy.Models.Request;
using PortalGalaxy.ViewModels;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Controllers;

public class TallerController : Controller
{
    private readonly ITallerProxy _proxy;
    private readonly ICategoriaProxy _categoriaProxy;
    private readonly ILogger<TallerController> _logger;

    private const int MaxFileSize = 4 * 1024 * 1024;

    public TallerController(ITallerProxy proxy, ICategoriaProxy categoriaProxy, ILogger<TallerController> logger)
    {
        _proxy = proxy;
        _categoriaProxy = categoriaProxy;
        _logger = logger;
    }

    // GET
    public async Task<IActionResult> Index(TallerViewModel model)
    {
        PaginationData pager = ViewBag.Pager != null
            ? ViewBag.Pager
            : new PaginationData();

        if (pager.CurrentPage == 0)
            pager.CurrentPage = model.Page <= 0 ? 1 : model.Page;

        pager.RowsPerPage = model.Rows <= 0 ? 5 : model.Rows;

        model.Categorias = await _categoriaProxy.ListAsync();

        var response = await _proxy.ListAsync(new BusquedaTallerRequest()
        {
            Filter = model.Nombre,
            CategoriaId = model.CategoriaSeleccionada,
            Situacion = model.SituacionSeleccionada,
            Page = pager.CurrentPage,
            Rows = pager.RowsPerPage
        });

        ViewBag.Pager = pager;

        if (response.Success)
        {
            model.Talleres = response.Data;
            pager.TotalPages = response.TotalPages;
            pager.RowCount = response.Data!.Count;
        }

        return View(model);
    }

    public async Task<IActionResult> Crear()
    {
        var vm = new FormTallerViewModel
        {
            Categorias = await _categoriaProxy.ListAsync(),
            Input = new TallerDtoRequest
            {
                FechaInicio = DateTime.Today,
                HoraInicio = DateTime.Now
            }
        };

        vm.BusquedaInstructorViewModel.Categorias = vm.Categorias;

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(FormTallerViewModel model)
    {
        // Primero obtenemos los archivos
        var archivos = HttpContext.Request.Form.Files;

        try
        {
            if (archivos.Any() && archivos.Count == 2)
            {
                var portada = archivos[0];
                var temario = archivos[1];

                if (portada.Length > MaxFileSize) // 4MB
                {
                    throw new InvalidOperationException("La imagen de la portada no puede ser mayor a 4MB");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await portada.CopyToAsync(memoryStream);

                    model.Input.Base64Portada = Convert.ToBase64String(memoryStream.ToArray());
                    model.Input.ArchivoPortada = portada.FileName;
                }

                using (var memoryStream = new MemoryStream())
                {
                    await temario.CopyToAsync(memoryStream);

                    model.Input.Base64Temario = Convert.ToBase64String(memoryStream.ToArray());
                    model.Input.ArchivoTemario = temario.FileName;
                }
                
            }

            await _proxy.CreateAsync(model.Input);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error al registrar el Taller {Message}", ex.Message);

            model.Categorias = await _categoriaProxy.ListAsync();
            model.BusquedaInstructorViewModel.Categorias = model.Categorias;
            
            ModelState.AddModelError("Error", ex.Message);
            return View(nameof(Crear), model);
        }
    }
}