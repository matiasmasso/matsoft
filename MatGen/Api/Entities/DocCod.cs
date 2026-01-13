using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class DocCod
{
    public Guid Guid { get; set; }

    public string Ord { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string? Obs { get; set; }

    public int? Id { get; set; }
}
