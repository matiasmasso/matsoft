using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwChannelSkusExcluded
{
    public Guid Channel { get; set; }

    public Guid Sku { get; set; }
}
