using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// External blog authors
/// </summary>
public partial class Blogger
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Blogger friendly name
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Blogger website
    /// </summary>
    public string Url { get; set; } = null!;

    /// <summary>
    /// Foreign key for Email table
    /// </summary>
    public Guid? Author { get; set; }

    public byte[]? Logo { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual Email? AuthorNavigation { get; set; }

    public virtual ICollection<Bloggerpost> Bloggerposts { get; set; } = new List<Bloggerpost>();
}
