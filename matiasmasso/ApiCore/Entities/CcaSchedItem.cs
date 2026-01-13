using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CcaSchedItem
{
    public Guid Guid { get; set; }

    public Guid Parent { get; set; }

    public Guid Cta { get; set; }

    public Guid? Contact { get; set; }

    public int Dh { get; set; }

    public decimal Eur { get; set; }

    public int? Lin { get; set; }

    public virtual CliGral? ContactNavigation { get; set; }

    public virtual PgcCtum CtaNavigation { get; set; } = null!;

    public virtual CcaSched ParentNavigation { get; set; } = null!;
}
