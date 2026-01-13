using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwDownloadTarget
{
    public Guid Target { get; set; }

    public string DocfileHash { get; set; } = null!;

    public int DocfileMime { get; set; }

    public int DocfileLength { get; set; }

    public int DocfilePags { get; set; }

    public int DocfileWidth { get; set; }

    public int DocfileHeight { get; set; }

    public int DocfileHres { get; set; }

    public int DocfileVres { get; set; }

    public DateTime DocfileFch { get; set; }

    public DateTime DocFileFchCreated { get; set; }

    public string? DocfileNom { get; set; }

    public bool? PublicarAlConsumidor { get; set; }

    public bool? PublicarAlDistribuidor { get; set; }

    public string? Lang { get; set; }

    public string Langset { get; set; } = null!;

    public bool Obsoleto { get; set; }

    public int ThumbnailMime { get; set; }
}
