using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Website home page banners
/// </summary>
public partial class Banner
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Date to be published from
    /// </summary>
    public DateTime FchFrom { get; set; }

    /// <summary>
    /// Last date to be published
    /// </summary>
    public DateTime? FchTo { get; set; }

    /// <summary>
    /// Landing page to link when someone clicks the banner
    /// </summary>
    public string? NavigateTo { get; set; }

    /// <summary>
    /// Product the banner refers to
    /// </summary>
    public Guid? Product { get; set; }

    /// <summary>
    /// Banner image
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// ISO639-2 language code
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// Date the image was uploaded
    /// </summary>
    public DateTime FchCreated { get; set; }
}
