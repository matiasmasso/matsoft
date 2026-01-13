using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Products and quantities expected to arrive from each import consignment
/// </summary>
public partial class ImportPrevisio
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Import consignment; foreign key to parent table ImportHdr
    /// </summary>
    public Guid Importacio { get; set; }

    /// <summary>
    /// Line number
    /// </summary>
    public int Lin { get; set; }

    /// <summary>
    /// Our order number
    /// </summary>
    public string? NumComandaProveidor { get; set; }

    /// <summary>
    /// Foreign key to Pnc table
    /// </summary>
    public Guid? PurchaseOrderItem { get; set; }

    /// <summary>
    /// Product Sku. Foreign key for Art table
    /// </summary>
    public Guid? Sku { get; set; }

    /// <summary>
    /// Our M+O product reference, unique per company. Stored in Art table Art field
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// Product Sku name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Units expected to arrive
    /// </summary>
    public int Qty { get; set; }

    /// <summary>
    /// Foreign key for InvoiceReceivedItem
    /// </summary>
    public Guid? InvoiceReceivedItem { get; set; }

    public virtual ImportHdr ImportacioNavigation { get; set; } = null!;

    public virtual Art? SkuNavigation { get; set; }
}
