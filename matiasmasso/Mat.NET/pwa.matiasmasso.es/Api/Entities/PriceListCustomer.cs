using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Retail price lists
    /// </summary>
    public partial class PriceListCustomer
    {
        public PriceListCustomer()
        {
            PriceListItemCustomers = new HashSet<PriceListItemCustomer>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Price list date. Only the most recent price list will be taken in account
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Termination date
        /// </summary>
        public DateTime? FchEnd { get; set; }
        /// <summary>
        /// In case it is a customer specific price list. Foreign key for CliGral table
        /// </summary>
        public Guid? Customer { get; set; }
        /// <summary>
        /// ISO 4217 currency code
        /// </summary>
        public string Currency { get; set; } = null!;
        /// <summary>
        /// Price list description
        /// </summary>
        public string? Concepte { get; set; }
        /// <summary>
        /// If true, visible to customers
        /// </summary>
        public bool? Visible { get; set; }

        public virtual CliGral? CustomerNavigation { get; set; }
        public virtual ICollection<PriceListItemCustomer> PriceListItemCustomers { get; set; }
    }
}
