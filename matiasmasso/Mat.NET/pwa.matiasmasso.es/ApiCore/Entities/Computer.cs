using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Computer
{
    public Guid Guid { get; set; }

    public string? Nom { get; set; }

    public string? Text { get; set; }

    public DateOnly? FchFrom { get; set; }

    public DateOnly? FchTo { get; set; }
}
