using System.ComponentModel.DataAnnotations;
using PortalGalaxy.Models.Response;

namespace PortalGalaxy.ViewModels;

public class TallerViewModel
{
    public string? Nombre { get; set; }

    public ICollection<CategoriaDtoResponse> Categorias { get; set; } = default!;

    [Display(Name = "Categoria")]
    public int? CategoriaSeleccionada { get; set; }

    [Display(Name = "Situacion")]
    public int? SituacionSeleccionada { get; set; }

    public ICollection<SituacionModel> Situaciones { get; } = new List<SituacionModel>()
    {
        new() { Codigo = 0, Nombre = "Por Aperturar" },
        new() { Codigo = 1, Nombre = "Aperturada" },
        new() { Codigo = 2, Nombre = "Concluida" },
        new() { Codigo = 3, Nombre = "Cancelada" },
    };

    public int Rows { get; set; }
    public int Page { get; set; }

    public ICollection<TallerDtoResponse>? Talleres { get; set; }
}