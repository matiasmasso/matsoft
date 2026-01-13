using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product resources like images or videos stored on the server filesystem by its hash signature
/// </summary>
public partial class MediaResource
{
    public Guid Guid { get; set; }

    /// <summary>
    /// Primary key
    /// </summary>
    public string Hash { get; set; } = null!;

    /// <summary>
    /// Descriptive name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Enumerable MatHelperStd.Mimecods
    /// </summary>
    public short Mime { get; set; }

    /// <summary>
    /// Enumerable DTOMediaResource.Cods (1.Product, 2.Features, 3.Life style)
    /// </summary>
    public short Cod { get; set; }

    /// <summary>
    /// ISO 639-2 language cod
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// Foreign key for brand Tpa table, product category Stp table or product sku Art table
    /// </summary>
    public Guid? Product { get; set; }

    /// <summary>
    /// Thumbnail image 140 x 140 pixels
    /// </summary>
    public byte[]? Thumbnail { get; set; }

    /// <summary>
    /// Image width
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Image height
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Length in bytes
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Horizontal resolution
    /// </summary>
    public int Hres { get; set; }

    /// <summary>
    /// Vertical resolution
    /// </summary>
    public int Vres { get; set; }

    /// <summary>
    /// Number of pages
    /// </summary>
    public int Pags { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public int Ord { get; set; }

    /// <summary>
    /// True if outdated
    /// </summary>
    public bool Obsoleto { get; set; }

    /// <summary>
    /// User who created this entry
    /// </summary>
    public Guid UsrCreated { get; set; }

    /// <summary>
    /// User who edited this entry for last time
    /// </summary>
    public Guid? UsrLastEdited { get; set; }

    /// <summary>
    /// Date the image was uploaded
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Date this entry was edited for last time
    /// </summary>
    public DateTime? FchLastEdited { get; set; }

    public string? DscEsp { get; set; }

    public string? DscCat { get; set; }

    public string? DscEng { get; set; }

    public string? DscPor { get; set; }

    public string LangSet { get; set; } = null!;

    public virtual ICollection<MediaResourceTarget> MediaResourceTargets { get; set; } = new List<MediaResourceTarget>();
}
