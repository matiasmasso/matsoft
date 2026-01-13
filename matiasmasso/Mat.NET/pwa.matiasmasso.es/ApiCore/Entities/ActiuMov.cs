using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class ActiuMov
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public Guid Actiu { get; set; }

    public DateOnly Fch { get; set; }

    public Guid? Contraparte { get; set; }

    public decimal? Qty { get; set; }

    public decimal Eur { get; set; }

    public virtual Actiu ActiuNavigation { get; set; } = null!;

    public virtual CliGral? ContraparteNavigation { get; set; }
}
