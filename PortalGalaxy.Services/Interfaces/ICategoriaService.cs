using PortalGalaxy.Models.Response;

namespace PortalGalaxy.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<BaseResponseGeneric<ICollection<CategoriaDtoResponse>>> ListAsync();
    }
}
