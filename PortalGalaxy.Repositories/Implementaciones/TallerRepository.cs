using Dapper;
using Microsoft.EntityFrameworkCore;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;

namespace PortalGalaxy.Repositories.Implementaciones
{
    public class TallerRepository : RepositoryBase<Taller>, ITallerRepository
    {
        public TallerRepository(PortalGalaxyDbContext context) 
            : base(context)
        {

        }

        public async Task<ICollection<Taller>> ListarTalleresAsync(string filtro, int categoriaId, int situacion, int page, int rows)
        {
            var connection = Context.Database.GetDbConnection();

            var query = await connection.QueryAsync<Taller>("uspListarTalleres", commandType: System.Data.CommandType.StoredProcedure, param: new
            {
                Nombre = filtro,
                CategoriaId = categoriaId,
                Situacion = situacion,
                Page = (page - 1) * rows,
                Rows = rows
            });

            return query.ToList();
        }
    }
}
