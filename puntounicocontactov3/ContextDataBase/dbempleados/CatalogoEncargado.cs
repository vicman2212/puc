using System;
using System.Collections.Generic;

namespace Webpuc;

/// <summary>
/// Catatogo del status que guarda el pemplado en cada area
/// </summary>
public partial class CatalogoEncargado
{
    public int IdCatalogoEncargado { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }
}
