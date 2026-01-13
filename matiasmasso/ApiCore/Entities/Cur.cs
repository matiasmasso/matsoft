using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Currency
/// </summary>
public partial class Cur
{
    /// <summary>
    /// Primary key; ISO 4217 3 digits currency code
    /// </summary>
    public string Tag { get; set; } = null!;

    /// <summary>
    /// Number of decimals used in this currency
    /// </summary>
    public byte? Decimals { get; set; }

    /// <summary>
    /// Symbol, if any
    /// </summary>
    public string? Symbol { get; set; }

    /// <summary>
    /// True if the currency is outdated
    /// </summary>
    public bool Obsoleto { get; set; }
}
