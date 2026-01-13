using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Doc
{
    public Guid Guid { get; set; }

    public string? Tit { get; set; }

    public string? Fch { get; set; }

    public byte[]? Thumbnail { get; set; }

    public byte[]? Stream { get; set; }

    public Guid? Cod { get; set; }

    public int OldCod { get; set; }

    public Guid? Src { get; set; }

    public int SrcOld { get; set; }

    public string? SrcDetail { get; set; }

    public string? ExternalUrl { get; set; }

    public int Mime { get; set; }

    public int Pags { get; set; }

    public int Size { get; set; }

    public string? Transcripcio { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string? Hash { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime FchLastEdited { get; set; }

    public string? Asin { get; set; }

    public virtual ICollection<DocTarget> DocTargets { get; set; } = new List<DocTarget>();

    public virtual DocFile? HashNavigation { get; set; }
}
