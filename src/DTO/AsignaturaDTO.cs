using System;
using System.Collections.Generic;

namespace InscripcionesApi.src.DTO;

public partial class AsignaturaDTO
{
    public int Id { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Creditos { get; set; }

}
