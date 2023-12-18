using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public int ModuloId { get; set; }

    public int PerfilId { get; set; }

    public string CodigoPermiso { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public byte Activo { get; set; }

    public virtual Modulo Modulo { get; set; } = null!;

    public virtual Perfil Perfil { get; set; } = null!;
}
