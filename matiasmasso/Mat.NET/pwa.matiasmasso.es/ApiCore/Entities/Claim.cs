using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Claim
{
    public Guid Guid { get; set; }

    public int Cod { get; set; }

    public virtual ICollection<Nav> Navs { get; set; } = new List<Nav>();
}
