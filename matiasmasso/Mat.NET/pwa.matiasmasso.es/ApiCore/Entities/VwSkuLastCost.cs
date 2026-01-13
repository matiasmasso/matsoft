using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwSkuLastCost
{
    public Guid SkuGuid { get; set; }

    public string SkuRef { get; set; } = null!;

    public Guid Proveidor { get; set; }

    public DateOnly? LastFch { get; set; }
}
