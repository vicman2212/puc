using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class ViewEmpleado
{
    public int NoEmpleado { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Correo { get; set; }

    public string? Extension { get; set; }

    public byte? StatusEmpleado { get; set; }

    public int IdPuesto { get; set; }

    public string Puesto { get; set; } = null!;

    public int IdDepartamento { get; set; }

    public string? NombreDepartamento { get; set; }

    public string Siglas { get; set; } = null!;

    public string? Calle { get; set; }

    public string? Conmutador { get; set; }

    public string? Numero { get; set; }

    public string? Localidad { get; set; }

    public string? CodigoPostal { get; set; }

    public string Municipio { get; set; } = null!;

    public int IdEntidadFederativa { get; set; }

    public string? EntidadFederativa { get; set; }

    public int? DepartamentoId { get; set; }

    public int? IdGerencia { get; set; }
}
