using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class EntidadFederativa
{
    public int IdEntidadFederativa { get; set; }

    public string? EntidadFederativa1 { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
