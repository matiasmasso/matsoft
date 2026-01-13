using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwBanc
{
    public int Emp { get; set; }

    public Guid Guid { get; set; }

    public string Abr { get; set; } = null!;

    public string Ccc { get; set; } = null!;

    public bool Obsoleto { get; set; }
}
