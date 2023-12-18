using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class ViewGerencium
{
    public int IdGerencia { get; set; }

    public int IdLocalidad { get; set; }

    public int? MunicipioId { get; set; }

    public int? EntidadFederativaId { get; set; }

    public int? TipoAsentamientoId { get; set; }

    public int? AsentaCpconsId { get; set; }

    public string? Localidad { get; set; }

    public string Municipio { get; set; } = null!;

    public string? EntidadFederativa { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Calle { get; set; }

    public string? Numero { get; set; }

    public string? Conmutador { get; set; }

    public string? MarcRapida { get; set; }
}
