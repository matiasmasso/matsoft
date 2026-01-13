using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwBrandVideo
{
    public Guid Brand { get; set; }

    public Guid Guid { get; set; }

    public string YoutubeId { get; set; } = null!;

    public string? Lang { get; set; }

    public int? ThumbnailMime { get; set; }

    public int? Duration { get; set; }

    public DateTime? FchTo { get; set; }

    public string? Tags { get; set; }

    public bool Obsoleto { get; set; }

    public DateTime FchCreated { get; set; }

    public string? NomEsp { get; set; }

    public string? NomCat { get; set; }

    public string? NomEng { get; set; }

    public string? NomPor { get; set; }

    public string? DscEsp { get; set; }

    public string? DscCat { get; set; }

    public string? DscEng { get; set; }

    public string? DscPor { get; set; }

    public string LangSet { get; set; } = null!;
}
