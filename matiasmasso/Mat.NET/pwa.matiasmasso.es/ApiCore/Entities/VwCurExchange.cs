using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCurExchange
{
    public string Tag { get; set; } = null!;

    public DateOnly Fch { get; set; }

    public decimal Rate { get; set; }
}
