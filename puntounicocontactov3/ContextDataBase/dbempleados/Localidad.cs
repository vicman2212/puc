using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Localidad
{
    public int IdLocalidad { get; set; }

    public int? MunicipioId { get; set; }

    public int? EntidadFederativaId { get; set; }

    public int? TipoAsentamientoId { get; set; }

    public int? AsentaCpconsId { get; set; }

    public string? Localidad1 { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Zona { get; set; }

    public virtual ICollection<Gerencium> Gerencia { get; set; } = new List<Gerencium>();

    public virtual Municipio? Municipio { get; set; }

    public virtual TipoAsentamiento? TipoAsentamiento { get; set; }
}
