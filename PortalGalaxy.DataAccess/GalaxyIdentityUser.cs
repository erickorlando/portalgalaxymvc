using Microsoft.AspNetCore.Identity;

namespace PortalGalaxy.DataAccess
{
    public class GalaxyIdentityUser : IdentityUser
    {
        public string NombreCompleto { get; set; } = default!;
    }
}
