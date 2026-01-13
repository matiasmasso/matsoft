using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Transport companies
/// </summary>
public partial class Trp
{
    /// <summary>
    /// Primary key; foreign key for CliGral table
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Abr { get; set; } = null!;

    /// <summary>
    /// chargeable weight / m3
    /// </summary>
    public int Cubicaje { get; set; }

    /// <summary>
    /// Template url for delivery tracking
    /// </summary>
    public string? TrackingUrlTemplate { get; set; }

    public virtual CliGral Gu { get; set; } = null!;
}
