using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

public partial class ClaseDTO
{
    public int Id { get; set; }

    public string? Codigo { get; set; } = null!;

    public int? IdProfesores { get; set; }

    public int? IdAsignaturas { get; set; }

    public int? MaxEstudiantes { get; set; }

    public virtual AsignaturaDTO? IdAsignaturasNavigation { get; set; }

    public virtual ProfesoreDTO? IdProfesoresNavigation { get; set; }

}
