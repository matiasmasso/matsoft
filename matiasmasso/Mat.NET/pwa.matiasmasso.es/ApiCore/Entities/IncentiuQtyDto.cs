using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Minimum units to reach the discount
/// </summary>
public partial class IncentiuQtyDto
{
    /// <summary>
    /// Foreign key for parent table Incentiu
    /// </summary>
    public Guid Incentiu { get; set; }

    /// <summary>
    /// Units
    /// </summary>
    public int Qty { get; set; }

    /// <summary>
    /// Discount, in case Incentiu.Cod sets this type of incentive
    /// </summary>
    public decimal Dto { get; set; }

    /// <summary>
    /// Free units to deliver, in case Incentiu.Cod sets this type of incentive
    /// </summary>
    public short FreeUnits { get; set; }

    public virtual Incentiu IncentiuNavigation { get; set; } = null!;
}
