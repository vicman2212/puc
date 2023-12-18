using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? NombreDepartamento { get; set; }

    public int? DepartamentoId { get; set; }

    public int? GerenciaId { get; set; }

    public string? NivelP { get; set; }

    public virtual Departamento? DepartamentoNavigation { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Gerencium? Gerencia { get; set; }

    public virtual ICollection<Departamento> InverseDepartamentoNavigation { get; set; } = new List<Departamento>();
}
