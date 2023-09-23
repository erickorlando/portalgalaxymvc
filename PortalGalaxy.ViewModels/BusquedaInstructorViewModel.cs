using System.ComponentModel.DataAnnotations;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.ViewModels;

public class BusquedaInstructorViewModel
{
    public string? Nombres { get; set; }

    [Display(Name="Nro. Documento")]
    public string? NroDocumento { get; set; }

    [Display(Name = "Categoria")]
    public int? CategoriaSeleccionada { get; set; }

    public ICollection<CategoriaDtoResponse> Categorias { get; set; } = new List<CategoriaDtoResponse>();

    public ICollection<InstructorDtoResponse>? Instructores { get; set; }

    public InstructorDtoResponse? InstructorSeleccionado { get; set; } 
}