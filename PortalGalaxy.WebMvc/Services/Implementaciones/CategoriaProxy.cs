using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Services.Implementaciones;

public class CategoriaProxy : CrudRestHelperBase<CategoriaDtoRequest, CategoriaDtoResponse>, ICategoriaProxy
{
    public CategoriaProxy(HttpClient httpClient) 
        : base("api/Categorias", httpClient)
    {
    }
}