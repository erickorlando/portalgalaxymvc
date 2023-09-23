using System.Data;
using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Entities;
using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Repositories.Interfaces;

namespace PortalGalaxy.Repositories.Implementaciones;

public class InstructorRepository : RepositoryBase<Instructor>, IInstructorRepository
{
    public InstructorRepository(PortalGalaxyDbContext context) 
        : base(context)
    {
    }

    public async Task<ICollection<InstructorInfo>> ListAsync(string? nombre, string? nroDocumento, int? categoriaId)
    {

        #region EF Core

        //Expression<Func<Instructor, bool>> predicate
        //    = x => x.Nombres.Contains(nombre ?? string.Empty)
        //                                    && (string.IsNullOrEmpty(nroDocumento) || x.NroDocumento.Equals(nroDocumento))
        //                                    && (categoriaId == null || x.CategoriaId.Equals(categoriaId));


        //return await Context.Set<Instructor>()
        //    .Where(predicate)
        //    .Select(p => new InstructorInfo
        //    {
        //        Id = p.Id,
        //        Nombres = p.Nombres,
        //        NroDocumento = p.NroDocumento,
        //        Categoria = p.Categoria.Nombre, // Lazy Loading - EF Core
        //        CategoriaId = p.CategoriaId
        //    })
        //    .ToListAsync(); 
        #endregion

        #region EF Core - Stored Procedure

        //var query = Context.Set<InstructorInfo>()
        //    .FromSqlRaw("EXEC uspListarInstructores {0}, {1}, {2}", nombre, nroDocumento, categoriaId);

        //return await query.ToListAsync();

        #endregion

        #region Dapper

        var query = await Context.Database.GetDbConnection().QueryAsync<InstructorInfo>("uspListarInstructores",
            commandType: CommandType.StoredProcedure,
            param: new
            {
                Nombres = nombre,
                NroDocumento = nroDocumento,
                CategoriaId = categoriaId
            });

        return query.ToList();

        #endregion
    }
}