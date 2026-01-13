using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Citum
{
    public Guid Guid { get; set; }

    public Guid? Pub { get; set; }

    public string? Author { get; set; }

    public int? Year { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public string? Container { get; set; }

    public string? Pags { get; set; }

    public string? Text { get; set; }

    public DateTime FchCreated { get; set; }
}
