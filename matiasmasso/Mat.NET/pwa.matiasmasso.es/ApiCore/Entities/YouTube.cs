using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product videos stored on Youtube
/// </summary>
public partial class YouTube
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Youtube Id of the video
    /// </summary>
    public string YoutubeId { get; set; } = null!;

    /// <summary>
    /// Video name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Video description
    /// </summary>
    public string? Dsc { get; set; }

    /// <summary>
    /// ISO 639-2 language code
    /// </summary>
    public string? Lang { get; set; }

    public byte[]? Thumbnail { get; set; }

    public int? ThumbnailMime { get; set; }

    public int? Duration { get; set; }

    public DateTime? FchTo { get; set; }

    public string? Tags { get; set; }

    /// <summary>
    /// True if the video is outdated
    /// </summary>
    public bool Obsoleto { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public string LangSet { get; set; } = null!;

    public virtual ICollection<YouTubeProduct> YouTubeProducts { get; set; } = new List<YouTubeProduct>();
}
