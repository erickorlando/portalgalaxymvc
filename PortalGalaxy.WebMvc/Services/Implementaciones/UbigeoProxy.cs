using PortalGalaxy.ViewModels;
using PortalGalaxy.WebMvc.Services.Interfaces;

namespace PortalGalaxy.WebMvc.Services.Implementaciones;

public class UbigeoProxy : IUbigeoProxy
{
    private readonly HttpClient _httpClient;

    public string UrlBase { get; set; } = default!;

    public UbigeoProxy()
    {
        _httpClient = new HttpClient();
    }

    public async Task<ICollection<DepartamentoModel>> ListarDepartamentos()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ICollection<DepartamentoModel>>($"{UrlBase}/data/departamentos.json");

        return response ?? new List<DepartamentoModel>();
    }

    public async Task<ICollection<ProvinciaModel>> ListarProvincias(string codDpto)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ICollection<ProvinciaModel>>($"{UrlBase}/data/provincias.json");

        var query = from filas in response
            where filas.CodigoDpto == codDpto
            select filas;

        return query.ToList();
    }

    public async Task<ICollection<DistritoModel>> ListarDistritos(string codProv)
    {
        var response = await _httpClient.GetFromJsonAsync<ICollection<DistritoModel>>($"{UrlBase}/data/distritos.json");

        var query = from filas in response
            where filas.CodProvincia == codProv
            select filas;

        return query.ToList();
    }
}