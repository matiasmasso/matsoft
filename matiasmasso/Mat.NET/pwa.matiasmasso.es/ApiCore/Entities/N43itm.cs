using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class N43itm
{
    public Guid Parent { get; set; }

    public int Cod { get; set; }

    public int Idx { get; set; }

    public string Body { get; set; } = null!;

    public virtual N43cca ParentNavigation { get; set; } = null!;
}
