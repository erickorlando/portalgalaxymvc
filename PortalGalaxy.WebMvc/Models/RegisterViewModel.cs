using Microsoft.AspNetCore.Mvc.Rendering;
using PortalGalaxy.Models;

namespace PortalGalaxy.WebMvc.Models;

public class RegisterViewModel
{
    public RegisterDtoRequest Input { get; set; } = default!;

    public List<SelectListItem> ListaDepartamentos { get; set; } = default!;
}