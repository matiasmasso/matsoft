using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerSkusIncluded
{
    public Guid Contact { get; set; }

    public Guid SkuGuid { get; set; }
}
