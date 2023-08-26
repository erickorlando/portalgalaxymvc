using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PortalGalaxy.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<GalaxyIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AspNetUsers
            builder.Entity<GalaxyIdentityUser>(e =>
            {
                e.ToTable("Usuario");
            });

            // AspNetRoles
            builder.Entity<IdentityRole>(e =>
            {
                e.ToTable("Rol");
            });

            // AspNetUserRoles
            builder.Entity<IdentityUserRole<string>>(e =>
            {
                e.ToTable("UsuarioRol");
            });


        }
    }
}
