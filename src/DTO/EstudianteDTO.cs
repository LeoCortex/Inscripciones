using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

public partial class EstudianteDTO
{
    public int Id { get; set; }

    public string? Identificacion { get; set; } = null!;

    public string? Nombres { get; set; } = null!;

    public string? Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public int? MaxAsignaturas { get; set; }

}
