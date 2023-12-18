using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Repetidosusr
{
    public short IdUsuario { get; set; }

    public short RolId { get; set; }

    public short NoEmpleado { get; set; }

    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string UltimoAcceso { get; set; } = null!;

    public string FechaRegistro { get; set; } = null!;

    public bool Activo { get; set; }

    public bool PassCtrl { get; set; }
}
