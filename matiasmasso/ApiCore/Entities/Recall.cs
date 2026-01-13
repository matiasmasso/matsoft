using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Manufacturer requests to recall defectuous products
/// </summary>
public partial class Recall
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Date of alert
    /// </summary>
    public DateOnly Fch { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Nom { get; set; } = null!;

    public virtual ICollection<RecallCli> RecallClis { get; set; } = new List<RecallCli>();
}
