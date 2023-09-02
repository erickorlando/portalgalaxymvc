using PortalGalaxy.Entities;

namespace PortalGalaxy.Repositories.Interfaces
{
    public interface ITallerRepository : IRepositoryBase<Taller>
    {
        Task<ICollection<Taller>> ListarTalleresAsync(string filtro, int categoriaId, int situacion, int page, int rows);

    }
}
