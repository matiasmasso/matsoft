using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class N43cca
{
    public Guid Guid { get; set; }

    public Guid Parent { get; set; }

    public Guid? Cca { get; set; }

    public virtual ICollection<N43itm> N43itms { get; set; } = new List<N43itm>();

    public virtual N43hdr ParentNavigation { get; set; } = null!;
}
