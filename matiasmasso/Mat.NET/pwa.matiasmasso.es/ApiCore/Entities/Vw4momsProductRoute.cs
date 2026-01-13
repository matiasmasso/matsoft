using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsProductRoute
{
    public Guid Brand { get; set; }

    public Guid? Category { get; set; }

    public Guid? Sku { get; set; }

    public string? Lang { get; set; }

    public string? CategorySegment { get; set; }

    public string? SkuSegment { get; set; }
}
