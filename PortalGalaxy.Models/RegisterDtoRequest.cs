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
        public string NroDocumento { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Compare(nameof(Password))]
        public string ConfirmarPassword { get; set; } = default!;

        [Display(Name = "Departamento")]
        public string CodigoDepartamento { get; set; } = default!;

        [Display(Name = "Provincia")]
        public string CodigoProvincia { get; set; } = default!;

        [Display(Name = "Distrito")]
        public string CodigoDistrito { get; set; } = default!;
    }
}
