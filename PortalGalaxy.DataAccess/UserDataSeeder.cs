using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PortalGalaxy.Common;

namespace PortalGalaxy.DataAccess
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            // Repositorio de Usuarios
            var userManager = service.GetRequiredService<UserManager<GalaxyIdentityUser>>();

            // Repositorio de Roles
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Crear Roles
            var adminRole = new IdentityRole(Constantes.RolAdmin);
            var customerRol = new IdentityRole(Constantes.RolAlumno);

            if (!await roleManager.RoleExistsAsync(Constantes.RolAdmin))
                await roleManager.CreateAsync(adminRole);

            if (!await roleManager.RoleExistsAsync(Constantes.RolAlumno))
                await roleManager.CreateAsync(customerRol);

            // Creamos el usuario Admin
            var adminUser = new GalaxyIdentityUser
            {
                NombreCompleto = "Administrador del Sistema",
                UserName = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "+1 999 999 999",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234*");
            if (result.Succeeded)
            {
                // Esto me asegura que el usuario se creó correctamente
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Constantes.RolAdmin);
            }
        }
    }
}
