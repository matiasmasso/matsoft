using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Tamariu
{
    public Guid Guid { get; set; }

    public DateTime? LastOk { get; set; }

    public DateTime? LastKo { get; set; }

    public DateTime? LastWarn { get; set; }
}
