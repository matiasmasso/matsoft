using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Supplier price lists
    /// </summary>
    public partial class PriceListSupplier
    {
        public PriceListSupplier()
        {
            PriceListItemSuppliers = new HashSet<PriceListItemSupplier>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Supplier, foreign key for CliGral table
        /// </summary>
        public Guid Proveidor { get; set; }
        /// <summary>
        /// Price list date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Price list description
        /// </summary>
        public string Concepte { get; set; } = null!;
        /// <summary>
        /// ISO 4217 currency code
        /// </summary>
        public string Currency { get; set; } = null!;
        /// <summary>
        /// Discount applicable on invoice
        /// </summary>
        public decimal DiscountOnInvoice { get; set; }
        /// <summary>
        /// Discount out of invoice like rapples etc
        /// </summary>
        public decimal DiscountOffInvoice { get; set; }
        /// <summary>
        /// Supplier document; foreign key for Docfile table
        /// </summary>
        public string? Hash { get; set; }

        public virtual DocFile? HashNavigation { get; set; }
        public virtual CliGral ProveidorNavigation { get; set; } = null!;
        public virtual ICollection<PriceListItemSupplier> PriceListItemSuppliers { get; set; }
    }
}
