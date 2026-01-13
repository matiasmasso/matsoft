using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwRetail
{
    public Guid SkuGuid { get; set; }

    public decimal? Retail { get; set; }
}
