using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product promoted on a blogger post
/// </summary>
public partial class BloggerpostProduct
{
    /// <summary>
    /// Foreign key to BloggerPost table
    /// </summary>
    public Guid Post { get; set; }

    /// <summary>
    /// Foreign key to product, either to brand Tpa table, category Stp table or sku Art table
    /// </summary>
    public Guid Product { get; set; }

    public virtual Bloggerpost PostNavigation { get; set; } = null!;
}
