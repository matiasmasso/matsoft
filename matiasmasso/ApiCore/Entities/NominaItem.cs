using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employee salary items
/// </summary>
public partial class NominaItem
{
    /// <summary>
    /// Accounts entry; foreign key for Cca table
    /// </summary>
    public Guid CcaGuid { get; set; }

    /// <summary>
    /// Item line, sequential number to sort the items
    /// </summary>
    public int Lin { get; set; }

    /// <summary>
    /// Concept code; foreign key for NominaConcepte table
    /// </summary>
    public int CodiConcepte { get; set; }

    /// <summary>
    /// U
    /// </summary>
    public int Qty { get; set; }

    /// <summary>
    /// Unit price
    /// </summary>
    public decimal Preu { get; set; }

    public virtual Nomina Cca { get; set; } = null!;

    public virtual NominaConcepte CodiConcepteNavigation { get; set; } = null!;
}
