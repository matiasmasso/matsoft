using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class ElCorteInglesAlineamientoStock
{
    public Guid Guid { get; set; }

    public DateTime? Fch { get; set; }

    public string? Text { get; set; }
}
