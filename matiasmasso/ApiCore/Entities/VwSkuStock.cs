using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwSkuStock
{
    public Guid SkuGuid { get; set; }

    public Guid? MgzGuid { get; set; }

    public int? Stock { get; set; }
}
