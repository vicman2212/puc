using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class TipoAsentamiento
{
    public int IdTipoAsentamiento { get; set; }

    public string? TipoAsentamiento1 { get; set; }

    public virtual ICollection<Localidad> Localidads { get; set; } = new List<Localidad>();
}
