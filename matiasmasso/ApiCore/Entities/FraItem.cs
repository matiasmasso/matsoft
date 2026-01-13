using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class FraItem
{
    public Guid Fra { get; set; }

    public int Cod { get; set; }

    public int Idx { get; set; }

    public string? Concept { get; set; }

    public decimal? Price { get; set; }

    public virtual Fra FraNavigation { get; set; } = null!;
}
