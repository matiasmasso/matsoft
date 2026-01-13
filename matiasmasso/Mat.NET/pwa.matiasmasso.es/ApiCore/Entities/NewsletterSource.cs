using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Posts referred to on each newsletter
/// </summary>
public partial class NewsletterSource
{
    /// <summary>
    /// Foreign key to parent table Newsletter 
    /// </summary>
    public Guid Newsletter { get; set; }

    /// <summary>
    /// Sort order of this post within same newsletter
    /// </summary>
    public int Ord { get; set; }

    /// <summary>
    /// Content; foreign key to either Noticia, BlogPost or Content table
    /// </summary>
    public Guid? SourceGuid { get; set; }

    /// <summary>
    /// Enumerable DTONewsletterSource.Cods (blog, news, event, promo...)
    /// </summary>
    public int SourceCod { get; set; }

    public virtual Newsletter NewsletterNavigation { get; set; } = null!;
}
