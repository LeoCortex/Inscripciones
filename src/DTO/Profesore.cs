using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

public partial class ProfesoreDTO
{
    public int Id { get; set; }

    public string? Identificacion { get; set; } = null!;

    public string? Nombres { get; set; } = null!;

    public string? Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

}
