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

    public TallerController(ITallerProxy proxy, ICategoriaProxy categoriaProxy)
    {
        _proxy = proxy;
        _categoriaProxy = categoriaProxy;
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
}