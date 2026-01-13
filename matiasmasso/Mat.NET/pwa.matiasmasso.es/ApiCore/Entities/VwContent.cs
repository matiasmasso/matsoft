using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwContent
{
    public Guid Guid { get; set; }

    public string? TitleEsp { get; set; }

    public string? TitleCat { get; set; }

    public string? TitleEng { get; set; }

    public string? TitlePor { get; set; }

    public string? ExcerptEsp { get; set; }

    public string? ExcerptCat { get; set; }

    public string? ExcerptEng { get; set; }

    public string? ExcerptPor { get; set; }

    public string? ContentEsp { get; set; }

    public string? ContentCat { get; set; }

    public string? ContentEng { get; set; }

    public string? ContentPor { get; set; }
}
