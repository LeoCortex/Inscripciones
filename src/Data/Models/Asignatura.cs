using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.Data.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Creditos { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
