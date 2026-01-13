using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Product range with distribution limited to selected customers
/// </summary>
public partial class PremiumLine
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Premium line name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Start date
    /// </summary>
    public DateTime? Fch { get; set; }

    /// <summary>
    /// DTOEnumerable (1.Include all customers not specifically excluded, 2.Disable all customers not specifically included) 
    /// </summary>
    public int Codi { get; set; }

    public virtual ICollection<PremiumCustomer> PremiumCustomers { get; set; } = new List<PremiumCustomer>();

    public virtual ICollection<PremiumProduct> PremiumProducts { get; set; } = new List<PremiumProduct>();

    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();
}
