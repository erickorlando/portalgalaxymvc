using Microsoft.EntityFrameworkCore;
using System.Reflection;
using PortalGalaxy.Entities.Infos;

namespace PortalGalaxy.DataAccess
{
    public class PortalGalaxyDbContext : DbContext
    {
        public PortalGalaxyDbContext(DbContextOptions<PortalGalaxyDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Se va a agregar la configuracion de las entidades desde este mismo ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<InstructorInfo>()
                .HasNoKey(); // Esto no es una tabla
        }
    }
}