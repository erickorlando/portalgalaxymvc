﻿namespace PortalGalaxy.Models.Request;

public class InstructorDtoRequest
{
    public string Nombres { get; set; } = default!;

    public string NroDocumento { get; set; } = default!;

    public int CategoriaId { get; set; }
}