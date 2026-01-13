using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwNoticia
{
    public Guid Guid { get; set; }

    public DateTime Fch { get; set; }

    public bool Visible { get; set; }

    public bool Professional { get; set; }

    public string? TitleEsp { get; set; }

    public string? TitleCat { get; set; }

    public string? TitleEng { get; set; }

    public string? TitlePor { get; set; }

    public string? UrlEsp { get; set; }

    public string? UrlCat { get; set; }

    public string? UrlEng { get; set; }

    public string? UrlPor { get; set; }
}
