using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Enlace
{
    public Guid Guid { get; set; }

    public Guid? Marit { get; set; }

    public Guid? Muller { get; set; }

    public int NupciesMarit { get; set; }

    public int NupciesMuller { get; set; }

    public string? FchQualifier { get; set; }

    public string? Fch { get; set; }

    public string? Fch2 { get; set; }

    public Guid? Cit { get; set; }

    public string? Obs { get; set; }

    public DateTime FchCreated { get; set; }

    public virtual Person? MaritNavigation { get; set; }

    public virtual Person? MullerNavigation { get; set; }
}
