using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Plantilla
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public string? Hash { get; set; }
}
