using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwBundleRetail
{
    public Guid Bundle { get; set; }

    public decimal? Retail { get; set; }
}
