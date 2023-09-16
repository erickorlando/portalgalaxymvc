using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.ViewModels;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Controllers;

public class ProvinciasController : Controller
{
    private readonly IUbigeoProxy _ubigeoProxy;

    public ProvinciasController(IUbigeoProxy ubigeoProxy)
    {
        _ubigeoProxy = ubigeoProxy;
    }

    // GET
    public async Task<IActionResult> Cargar(string codDepartamento)
    {
        _ubigeoProxy.UrlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        var provincias = new List<ProvinciaModel>()
        {
            new()
            {
                Codigo = "00",
                Nombre = "-Seleccione-"
            }
        };

        provincias.AddRange(await _ubigeoProxy.ListarProvincias(codDepartamento));

        return Json(provincias);
    }
}