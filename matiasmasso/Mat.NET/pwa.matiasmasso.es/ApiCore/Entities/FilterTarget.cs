using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Products where filter options are applicable
/// </summary>
public partial class FilterTarget
{
    /// <summary>
    /// Filter option; foreign key for FilterItem
    /// </summary>
    public Guid FilterItem { get; set; }

    /// <summary>
    /// Product where this option is applicable
    /// </summary>
    public Guid Target { get; set; }

    public virtual FilterItem FilterItemNavigation { get; set; } = null!;
}
