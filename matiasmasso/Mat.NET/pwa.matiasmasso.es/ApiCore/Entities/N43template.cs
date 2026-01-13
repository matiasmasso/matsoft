using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class N43template
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public string Pattern { get; set; } = null!;

    public string? Concepte { get; set; }

    public Guid? Cta { get; set; }

    public Guid? Contact { get; set; }

    public Guid? Projecte { get; set; }
}
