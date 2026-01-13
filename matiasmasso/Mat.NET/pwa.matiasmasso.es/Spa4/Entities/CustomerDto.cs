using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Discount from Retail Price list VAT included a customer gets for a product range in order to generate his cost price list VAT excluded. Newer entries always override older entries for each customer and product range
    /// </summary>
    public partial class CustomerDto
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key for CliGral table
        /// </summary>
        public Guid Customer { get; set; }
        /// <summary>
        /// Date. Further recent entries of same customer/product range override older ones
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Product range. It may be a brand (Tpa table), a brand category (FK for Stp), or a single product sku (Art table)
        /// </summary>
        public Guid? Product { get; set; }
        /// <summary>
        /// Discount over retail price list VAT included to get customer cost VAT excluded
        /// </summary>
        public decimal? Dto { get; set; }
        /// <summary>
        /// Comments justifying this discount, if any
        /// </summary>
        public string? Obs { get; set; }

        public virtual CliGral CustomerNavigation { get; set; } = null!;
    }
}
