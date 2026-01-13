using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwChannelDto
{
    public Guid Guid { get; set; }

    public Guid Channel { get; set; }

    public Guid? Product { get; set; }

    public decimal Dto { get; set; }

    public DateTime Fch { get; set; }

    public string? Obs { get; set; }
}
