using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Invoice item that generates a sale commission to a specific rep
/// </summary>
public partial class Rp
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Agent. Foreign key for CliGral table
    /// </summary>
    public Guid RepGuid { get; set; }

    /// <summary>
    /// Invoice. Foreign key to Fra table
    /// </summary>
    public Guid FraGuid { get; set; }

    /// <summary>
    /// Commission statement. Foreign key to RepLiq table
    /// </summary>
    public Guid? RepLiqGuid { get; set; }

    /// <summary>
    /// Base amount over which the commission applies
    /// </summary>
    public decimal Bas { get; set; }

    /// <summary>
    /// Commission value
    /// </summary>
    public decimal ComVal { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// if true, commission will be granted to the agent
    /// </summary>
    public bool? Liquidable { get; set; }

    public virtual Fra Fra { get; set; } = null!;

    public virtual RepLiq? RepLiq { get; set; }
}
