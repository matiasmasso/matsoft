using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Commercial terms and conditions
/// </summary>
public partial class Cond
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    public virtual ICollection<CondCapitol> CondCapitols { get; set; } = new List<CondCapitol>();
}
