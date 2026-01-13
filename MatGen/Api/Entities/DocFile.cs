using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class DocFile
{
    public string Hash { get; set; } = null!;

    public int? Size { get; set; }

    public int? Pags { get; set; }

    public byte[]? Stream { get; set; }

    public int StreamMime { get; set; }

    public byte[]? Thumbnail { get; set; }

    public int ThumbnailMime { get; set; }

    public DateTime FchCreated { get; set; }

    public string? Sha256 { get; set; }

    public virtual ICollection<DocSrc> DocSrcs { get; set; } = new List<DocSrc>();

    public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();

    public virtual ICollection<Pub> Pubs { get; set; } = new List<Pub>();
}
