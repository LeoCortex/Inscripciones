using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.Data.Models;

public partial class Profesore
{
    public int Id { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
