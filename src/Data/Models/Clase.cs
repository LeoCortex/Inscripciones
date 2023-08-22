using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.Data.Models;

public partial class Clase
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public int IdProfesores { get; set; }

    public int IdAsignaturas { get; set; }

    public int? MaxEstudiantes { get; set; }

    public virtual Asignatura IdAsignaturasNavigation { get; set; } = null!;

    public virtual Profesore IdProfesoresNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
