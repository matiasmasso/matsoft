using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Customer numbers assigned by our suppliers to our customers. Used to automate Excel files for example Mayborn requests to supply his Pharma channel distributors
/// </summary>
public partial class PrvCliNum
{
    /// <summary>
    /// Supplier. Foreign key to CliGral table
    /// </summary>
    public Guid Proveidor { get; set; }

    /// <summary>
    /// Customer number assigned by our supplier to our customer
    /// </summary>
    public string CliNum { get; set; } = null!;

    /// <summary>
    /// Customer whom belongs this customer number assigned by this supplier. Foreign key for CliGral table
    /// </summary>
    public Guid Customer { get; set; }

    public virtual CliGral ProveidorNavigation { get; set; } = null!;
}
