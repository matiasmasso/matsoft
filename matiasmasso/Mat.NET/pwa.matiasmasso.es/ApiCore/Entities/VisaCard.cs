using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employees credit cards
/// </summary>
public partial class VisaCard
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    /// <summary>
    /// Employee. Foreign key for CliGral table
    /// </summary>
    public Guid Titular { get; set; }

    /// <summary>
    /// Credit card number
    /// </summary>
    public string Digits { get; set; } = null!;

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Alias { get; set; } = null!;

    /// <summary>
    /// Expiration date
    /// </summary>
    public string Caduca { get; set; } = null!;

    /// <summary>
    /// Credit limit
    /// </summary>
    public decimal Limit { get; set; }

    /// <summary>
    /// Bank issuer; foreign key for CliBnc table
    /// </summary>
    public Guid Banc { get; set; }

    /// <summary>
    /// Cancelation date, if any
    /// </summary>
    public DateOnly? FchCanceled { get; set; }

    /// <summary>
    /// Issuer company
    /// </summary>
    public Guid Emisor { get; set; }

    /// <summary>
    /// Safety code
    /// </summary>
    public string? Ccid { get; set; }

    public virtual CliBnc BancNavigation { get; set; } = null!;

    public virtual VisaEmisor EmisorNavigation { get; set; } = null!;

    public virtual CliGral TitularNavigation { get; set; } = null!;
}
