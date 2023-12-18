using System;
using System.Collections.Generic;

namespace Webpuc;

/// <summary>
/// Aplicaciones del sistema para Acceso
/// </summary>
public partial class Modulo
{
    public int IdModulo { get; set; }

    public string CodigoModulo { get; set; } = null!;

    public string? NombreModulo { get; set; }

    public string? Ruta { get; set; }

    public string Modulo1 { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? Descripcion { get; set; }

    public string? Icon { get; set; }

    public byte Activo { get; set; }

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
