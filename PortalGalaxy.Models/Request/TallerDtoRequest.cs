namespace PortalGalaxy.Models.Request
{
    public class TallerDtoRequest
    {
        public string Nombre { get; set; } = default!;

        public int CategoriaId { get; set; }

        public int InstructorId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime HoraInicio { get; set; }

        public int Situacion { get; set; }
        public string? UrlPortada { get; set; }
        public string? UrlTemario { get; set; }
    }
}
