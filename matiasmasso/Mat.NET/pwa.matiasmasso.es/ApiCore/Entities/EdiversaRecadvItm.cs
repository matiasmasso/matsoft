using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class EdiversaRecadvItm
{
    public Guid Parent { get; set; }

    public int Lin { get; set; }

    public string? Ean { get; set; }

    public string? Pialin { get; set; }

    public int? QtyLin { get; set; }
}
