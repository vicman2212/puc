using System;
using System.Collections.Generic;

namespace Webpuc;

public partial class Gerencium
{
    public int IdGerencia { get; set; }

    public string? Calle { get; set; }

    public string? Numero { get; set; }

    public string? Conmutador { get; set; }

    public string? MarcRapida { get; set; }

    public int? LocalidadId { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

    public virtual Localidad? Localidad { get; set; }
}
