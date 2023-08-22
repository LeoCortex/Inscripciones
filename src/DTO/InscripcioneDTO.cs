using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

public partial class InscripcioneDTO
{
    public int Id { get; set; }

    public string? Codigo { get; set; }

    public int? IdEstudiantes { get; set; }

    public int? IdClases { get; set; }
    public  ClaseDTO? IdClasesNavigation { get; set; }
    public  EstudianteDTO? IdEstudiantesNavigation { get; set; }

}
