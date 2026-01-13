using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwTaskLastLog
{
    public Guid Guid { get; set; }

    public Guid Task { get; set; }

    public DateTimeOffset? Fch { get; set; }

    public int ResultCod { get; set; }

    public string? ResultMsg { get; set; }
}
