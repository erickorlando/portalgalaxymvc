using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Controllers;

public class CategoriaController : Controller
{
    private readonly ICategoriaProxy _proxy;

    public CategoriaController(ICategoriaProxy proxy)
    {
        _proxy = proxy;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _proxy.ListAsync());
    }
}