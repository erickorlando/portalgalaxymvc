using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.Models
{
    public class RegisterDtoRequest
    {
        [Required]
        public string Usuario { get; set; } = default!;

        [Required]
        public string NombreCompleto { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Telefono { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Compare(nameof(Password))]
        public string ConfirmarPassword { get; set; } = default!;
    }
}
