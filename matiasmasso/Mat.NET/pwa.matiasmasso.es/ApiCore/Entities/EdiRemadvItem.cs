using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Documents included on each remittance advice
/// </summary>
public partial class EdiRemadvItem
{
    /// <summary>
    /// Foreign key to parent table EdiRemadvHeader
    /// </summary>
    public Guid Parent { get; set; }

    /// <summary>
    /// Sort order within same remittance
    /// </summary>
    public int Idx { get; set; }

    /// <summary>
    /// Enumerable DTOEdiversaRemadvItem.Types
    /// </summary>
    public int ItemType { get; set; }

    /// <summary>
    /// Document concept
    /// </summary>
    public string ItemNom { get; set; } = null!;

    /// <summary>
    /// Document number
    /// </summary>
    public string ItemNum { get; set; } = null!;

    /// <summary>
    /// Daocument date
    /// </summary>
    public DateOnly? ItemFch { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string ItemCur { get; set; } = null!;

    /// <summary>
    /// Document amount
    /// </summary>
    public decimal ItemAmt { get; set; }

    /// <summary>
    /// Debt; foreign key to Pnd table
    /// </summary>
    public Guid? PndGuid { get; set; }

    public virtual EdiRemadvHeader ParentNavigation { get; set; } = null!;
}
