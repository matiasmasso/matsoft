using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCnapParent
{
    public Guid? Parent { get; set; }

    public Guid? Child { get; set; }
}
