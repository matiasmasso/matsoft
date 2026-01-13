using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class PromofarmaFeed
{
    public Guid Customer { get; set; }

    public Guid Sku { get; set; }

    public string? Id { get; set; }

    public bool? Enabled { get; set; }
}
