using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwMgzInventoryCost
{
    public Guid? MgzGuid { get; set; }

    public Guid ArtGuid { get; set; }

    public Guid AlbGuid { get; set; }

    public int Alb { get; set; }

    public DateTime Fch { get; set; }

    public int Qty { get; set; }

    public decimal? Eur { get; set; }

    public decimal Dto { get; set; }

    public decimal Dt2 { get; set; }
}
