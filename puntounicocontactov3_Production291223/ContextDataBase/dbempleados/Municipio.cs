using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Municipio
{
    public int IdMunicipio { get; set; }

    public int EntidadFederativaId { get; set; }

    public string Municipio1 { get; set; } = null!;

    public virtual EntidadFederativa EntidadFederativa { get; set; } = null!;

    public virtual ICollection<Localidad> Localidads { get; set; } = new List<Localidad>();
}
