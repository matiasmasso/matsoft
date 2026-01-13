using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwDoc
{
    public Guid Guid { get; set; }

    public string? Tit { get; set; }

    public string? Fch { get; set; }

    public Guid? Cod { get; set; }

    public Guid? Src { get; set; }

    public string? SrcDetail { get; set; }

    public string? ExternalUrl { get; set; }

    public string? Hash { get; set; }

    public int? StreamMime { get; set; }

    public int? ThumbnailMime { get; set; }

    public int? Pags { get; set; }

    public int? Size { get; set; }

    public DateTime FchCreated { get; set; }
}
