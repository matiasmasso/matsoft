using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Import consignment documents
/// </summary>
public partial class ImportDtl
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key for parent table ImportHdr
    /// </summary>
    public Guid? HeaderGuid { get; set; }

    /// <summary>
    /// Type of document. Enumerable DTOImportacioItem.SourceCodes
    /// </summary>
    public int SrcCod { get; set; }

    /// <summary>
    /// Document description
    /// </summary>
    public string? Descripcio { get; set; }

    /// <summary>
    /// Value in Euros, if applicable
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Value in foreign currency, if applicable
    /// </summary>
    public decimal Div { get; set; }

    /// <summary>
    /// Pdf document. Foreign key for DocFile table
    /// </summary>
    public string? Hash { get; set; }

    public virtual DocFile? HashNavigation { get; set; }

    public virtual ImportHdr? Header { get; set; }
}
