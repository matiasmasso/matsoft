using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product range with distribution limited to selected customers
    /// </summary>
    public partial class PremiumLine
    {
        public PremiumLine()
        {
            PremiumCustomers = new HashSet<PremiumCustomer>();
            PremiumProducts = new HashSet<PremiumProduct>();
            Webs = new HashSet<Web>();
        }

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

        public virtual ICollection<PremiumCustomer> PremiumCustomers { get; set; }
        public virtual ICollection<PremiumProduct> PremiumProducts { get; set; }
        public virtual ICollection<Web> Webs { get; set; }
    }
}
