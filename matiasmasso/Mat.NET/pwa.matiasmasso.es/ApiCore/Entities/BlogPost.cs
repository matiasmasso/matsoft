using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Blog posts
/// </summary>
public partial class BlogPost
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    public DateTime? Fch { get; set; }

    /// <summary>
    /// Featured image
    /// </summary>
    public byte[]? Thumbnail { get; set; }

    /// <summary>
    /// If false, the post will be hidden to blog visitors
    /// </summary>
    public bool Visible { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime? FchCreated { get; set; }

    /// <summary>
    /// User who created this entry
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// Date this entry was last edited
    /// </summary>
    public DateTime? FchLastEdited { get; set; }

    /// <summary>
    /// User who edited this entry for last time
    /// </summary>
    public Guid? UsrLastEdited { get; set; }

    public virtual Email? UsrCreatedNavigation { get; set; }

    public virtual Email? UsrLastEditedNavigation { get; set; }
}
