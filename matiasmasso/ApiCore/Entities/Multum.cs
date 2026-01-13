using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Penalty fees
/// </summary>
public partial class Multum
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Penalty issuer, foreign key for CliGral table
    /// </summary>
    public Guid? Emisor { get; set; }

    /// <summary>
    /// Proceeding number, issuer reference
    /// </summary>
    public string? Expedient { get; set; }

    /// <summary>
    /// Penalty object, usually a car from the company fleet
    /// </summary>
    public Guid? Subjecte { get; set; }

    /// <summary>
    /// Infringement date
    /// </summary>
    public DateOnly Fch { get; set; }

    /// <summary>
    /// Payment deadline
    /// </summary>
    public DateOnly? Vto { get; set; }

    /// <summary>
    /// Payment date
    /// </summary>
    public DateOnly? Pagat { get; set; }

    /// <summary>
    /// Penalty fee
    /// </summary>
    public decimal Eur { get; set; }

    public virtual CliGral? EmisorNavigation { get; set; }
}
