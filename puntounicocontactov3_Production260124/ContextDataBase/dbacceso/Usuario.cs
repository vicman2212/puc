using System;
using System.Collections.Generic;

namespace Webpuc;

/// <summary>
/// Usuarios de acceso a los modulos
/// </summary>
public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int RolId { get; set; }

    public int NoEmpleado { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public DateTime? UltimoAcceso { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public byte Activo { get; set; }

    public byte PassCtrl { get; set; }
}
