using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCliTpaExcludedCustomer
{
    public Guid Customer { get; set; }

    public Guid Sku { get; set; }
}
