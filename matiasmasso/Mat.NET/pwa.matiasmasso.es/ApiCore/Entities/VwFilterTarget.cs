using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwFilterTarget
{
    public Guid ParentProduct { get; set; }

    public Guid FilterItem { get; set; }
}
