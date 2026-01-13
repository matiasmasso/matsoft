using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwRetailPrice
{
    public Guid Art { get; set; }

    public DateOnly Fch { get; set; }

    public DateOnly? FchEnd { get; set; }

    public decimal? Retail { get; set; }

    public Guid? Customer { get; set; }
}
