using PortalGalaxy.ViewModels;

namespace PortalGalaxy.WebMvc.Services.Interfaces;

public interface IUbigeoProxy
{
    public string UrlBase { get; set; }

    Task<ICollection<DepartamentoModel>> ListarDepartamentos();

    Task<ICollection<ProvinciaModel>> ListarProvincias(string codDpto);

    Task<ICollection<DistritoModel>> ListarDistritos(string codProv);
}