using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
