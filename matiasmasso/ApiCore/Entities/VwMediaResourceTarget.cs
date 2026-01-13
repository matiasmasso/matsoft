using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwMediaResourceTarget
{
    public Guid MediaResource { get; set; }

    public Guid Brand { get; set; }

    public Guid? Category { get; set; }

    public Guid? Sku { get; set; }

    public bool Obsoleto { get; set; }
}
