using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Controllers;

public class DistritosController : Controller
{
    private readonly IUbigeoProxy _ubigeoProxy;

    public DistritosController(IUbigeoProxy ubigeoProxy)
    {
        _ubigeoProxy = ubigeoProxy;
    }

    // GET
    public async Task<IActionResult> Cargar(string codProvincia)
    {
        _ubigeoProxy.UrlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        return Json(await _ubigeoProxy.ListarDistritos(codProvincia));
    }
}