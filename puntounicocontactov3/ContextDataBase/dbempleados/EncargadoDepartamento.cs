using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class EncargadoDepartamento
{
    public int IdEncargadoDepartamento { get; set; }

    public int? DepartamentoId { get; set; }

    public int? EmpleadoId { get; set; }

    public DateTime? FechaEncargo { get; set; }

    public int? StatusEncargo { get; set; }
}
