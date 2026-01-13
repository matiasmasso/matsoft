using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Stores system default values
/// </summary>
public partial class Default
{
    /// <summary>
    /// Company, foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Enumerable DTODefault.Codis
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// Default value for this code on this company
    /// </summary>
    public string Value { get; set; } = null!;
}
