using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.Models.Request
{
    public class TallerDtoRequest
    {
        public string Nombre { get; set; } = default!;

        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Hora de Inicio")]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "Situacion")]
        public int Situacion { get; set; }

        [Display(Name = "Portada")]
        public string? Base64Portada { get; set; }

        [Display(Name = "Temario")]
        public string? Base64Temario { get; set; }

        public string? PortadaUrl { get; set; }
        public string? TemarioUrl { get; set; }
        public string? ArchivoPortada { get; set; }
        public string? ArchivoTemario { get; set; }
    }
}
