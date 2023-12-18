using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Rol
{
    public int IdRol { get; set; }

    public int PerfilId { get; set; }

    public string CodigoRol { get; set; } = null!;

    public string? Rol1 { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Descripcion { get; set; }

    public byte Activo { get; set; }

    public virtual Perfil Perfil { get; set; } = null!;
}
