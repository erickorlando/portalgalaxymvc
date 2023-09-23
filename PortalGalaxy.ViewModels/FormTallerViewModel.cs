using PortalGalaxy.Models.Request;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.ViewModels;

public class FormTallerViewModel
{
    public TallerDtoRequest Input { get; set; } = default!;

    public ICollection<CategoriaDtoResponse> Categorias { get; set; } = default!;

    public ICollection<SituacionModel> Situaciones { get; } = new List<SituacionModel>()
    {
        new() { Codigo = 0, Nombre = "Por Aperturar" },
        new() { Codigo = 1, Nombre = "Aperturada" },
        new() { Codigo = 2, Nombre = "Concluida" },
        new() { Codigo = 3, Nombre = "Cancelada" },
    };

    public BusquedaInstructorViewModel BusquedaInstructorViewModel { get; set; } = new();
}