using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class TmpChild
{
    public Guid Guid { get; set; }

    public int Parent { get; set; }

    public string? Name { get; set; }
}
