using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class DocTarget
{
    public Guid Guid { get; set; }

    public Guid Doc { get; set; }

    public Guid Target { get; set; }

    public int TargetCod { get; set; }

    public Guid Rel { get; set; }

    public Guid? Cit { get; set; }

    public bool Difunt { get; set; }

    public string? Obs { get; set; }

    public bool SubjectePassiu { get; set; }

    public DateTime FchCreated { get; set; }

    public virtual Doc DocNavigation { get; set; } = null!;

    public virtual DocRel RelNavigation { get; set; } = null!;
}
