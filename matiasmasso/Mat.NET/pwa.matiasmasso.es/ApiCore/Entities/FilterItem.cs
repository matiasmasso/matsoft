using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Options for product filters
/// </summary>
public partial class FilterItem
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key for parent table Filter
    /// </summary>
    public Guid Filter { get; set; }

    /// <summary>
    /// Sort order in which this option should appear within its filter
    /// </summary>
    public int Ord { get; set; }

    public virtual Filter FilterNavigation { get; set; } = null!;

    public virtual ICollection<FilterTarget> FilterTargets { get; set; } = new List<FilterTarget>();
}
