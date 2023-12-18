using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class TituloProfesional
{
    public int IdTituloProfesional { get; set; }

    public string Titulo { get; set; } = null!;

    public string Siglas { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
