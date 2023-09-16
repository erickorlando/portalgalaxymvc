using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Services.Implementaciones;

public class TallerProxy : CrudRestHelperBase<TallerDtoRequest, TallerDtoResponse>, ITallerProxy
{
    public TallerProxy(HttpClient httpClient) 
        : base("api/Talleres", httpClient)
    {
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request)
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TallerDtoResponse>>(
            $"{BaseUrl}?filter={request.Filter}&categoriaId={request.CategoriaId}&situacion={request.Situacion}&page={request.Page}&rows={request.Rows}");

        if (response is { Success: true })
        {
            return response;
        }

        return await Task.FromResult(new PaginationResponse<TallerDtoResponse>());
    }
}