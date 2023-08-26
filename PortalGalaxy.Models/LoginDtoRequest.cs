using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.Models
{
    public class LoginDtoRequest
    {
        [Required]
        public string Usuario { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
