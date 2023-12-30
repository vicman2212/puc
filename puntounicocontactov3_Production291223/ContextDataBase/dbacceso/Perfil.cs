using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Perfil
{
    public int IdPerfil { get; set; }

    public string CodigoPerfil { get; set; } = null!;

    public string Perfil1 { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public string? Descripcion { get; set; }

    public byte Activo { get; set; }

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
