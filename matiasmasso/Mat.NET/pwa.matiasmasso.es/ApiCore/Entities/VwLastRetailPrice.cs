using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwLastRetailPrice
{
    public Guid PriceList { get; set; }

    public Guid Art { get; set; }

    public DateOnly Fch { get; set; }

    public decimal? Retail { get; set; }
}
