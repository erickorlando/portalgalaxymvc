namespace PortalGalaxy.Models.Request;

public class BusquedaTallerRequest : RequestBase
{
    public string? Filter { get; set; }
    public int? CategoriaId { get; set; }
    public int? Situacion { get; set; }
}