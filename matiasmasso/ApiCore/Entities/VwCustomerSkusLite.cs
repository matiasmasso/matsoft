using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerSkusLite
{
    public Guid SkuGuid { get; set; }

    public Guid Customer { get; set; }

    public Guid? Ccx { get; set; }

    public int ExclusionCod { get; set; }
}
