using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwSkuBundleRetail
{
    public Guid SkuGuid { get; set; }

    public decimal? Retail { get; set; }
}
