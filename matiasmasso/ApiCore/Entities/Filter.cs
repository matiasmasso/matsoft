using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product features filters
/// </summary>
public partial class Filter
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Sort order where this filter should be displayed
    /// </summary>
    public int Ord { get; set; }

    public virtual ICollection<FilterItem> FilterItems { get; set; } = new List<FilterItem>();
}
