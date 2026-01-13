using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwWtbolInventory
{
    public Guid Site { get; set; }

    public decimal? Inventory { get; set; }
}
