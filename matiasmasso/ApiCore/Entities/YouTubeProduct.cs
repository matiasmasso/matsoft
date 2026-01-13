using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Videos per product
/// </summary>
public partial class YouTubeProduct
{
    /// <summary>
    /// Foreign key to parent table YouTube with video details
    /// </summary>
    public Guid YouTubeGuid { get; set; }

    /// <summary>
    /// Product; foreign key either brand Tpa table, category Stp table or Sku Art table depending on video purpose
    /// </summary>
    public Guid ProductGuid { get; set; }

    /// <summary>
    /// Sort order this video should appear on this product list
    /// </summary>
    public int Ord { get; set; }

    public virtual YouTube YouTube { get; set; } = null!;
}
