using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Amortisation rates
/// </summary>
public partial class MrtTipu
{
    /// <summary>
    /// Account code. Enumerable DTOPgcPlan.Ctas
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    public int Pct { get; set; }
}
