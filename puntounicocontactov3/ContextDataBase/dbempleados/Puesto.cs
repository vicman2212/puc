using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Puesto
{
    public int IdPuesto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Puesto1 { get; set; } = null!;

    public string Nivel { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
