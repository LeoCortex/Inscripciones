using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.Data.Models;

public partial class Inscripcione
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public int IdEstudiantes { get; set; }

    public int IdClases { get; set; }

    public virtual Clase IdClasesNavigation { get; set; } = null!;

    public virtual Estudiante IdEstudiantesNavigation { get; set; } = null!;
}
