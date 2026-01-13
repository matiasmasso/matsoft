using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerSkusExcluded
{
    public Guid Customer { get; set; }

    public Guid Sku { get; set; }

    public int Cod { get; set; }
}
