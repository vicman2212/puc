using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int NoEmpleado { get; set; }

    public int PuestoId { get; set; }

    public int DepartamentoId { get; set; }

    public int? TituloId { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Extension { get; set; }

    public byte StatusEmpleado { get; set; }

    /// <summary>
    /// 0, el empleado esta delegado en el departamento; 1, El Empleado no esta delegado, su gerencia es la original
    /// </summary>
    public byte? Delegado { get; set; }

    public string? Rfc { get; set; }

    public string? Curp { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual Puesto Puesto { get; set; } = null!;

    public virtual TituloProfesional? Titulo { get; set; }
}
