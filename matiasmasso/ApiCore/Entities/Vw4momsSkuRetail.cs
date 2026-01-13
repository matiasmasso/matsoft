using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsSkuRetail
{
    public Guid Brand { get; set; }

    public Guid SkuGuid { get; set; }

    public decimal? Retail { get; set; }
}
