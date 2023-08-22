using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

/// <summary>
/// objeto para manejar las consultas de estudiantes que comparten la misma clase
/// </summary> <summary>
/// 
/// </summary>
public partial class EstudianteCompDTO
{
    public int Id { get; set; }

    public string? Identificacion { get; set; } = null!;

    public string? Nombres { get; set; } = null!;

    public string? Apellidos { get; set; } = null!;
    
    public string? ProfesorNombre { get; set; }
    
    public string? AsignaturaNombre { get; set; }

}
