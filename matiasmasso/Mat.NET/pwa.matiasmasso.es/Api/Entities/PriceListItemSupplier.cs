using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Supplier price list items
    /// </summary>
    public partial class PriceListItemSupplier
    {
        /// <summary>
        /// Foreign key for parent table PriceList_Supplier
        /// </summary>
        public Guid PriceList { get; set; }
        /// <summary>
        /// Manufacturer product code
        /// </summary>
        public string Ref { get; set; } = null!;
        /// <summary>
        /// Sort order
        /// </summary>
        public int? Lin { get; set; }
        /// <summary>
        /// EAN 13 product code
        /// </summary>
        public string? Ean { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Distributor price list
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Retail price list
        /// </summary>
        public decimal? Retail { get; set; }
        /// <summary>
        /// Units per package
        /// </summary>
        public int? InnerPack { get; set; }

        public virtual PriceListSupplier PriceListNavigation { get; set; } = null!;
    }
}
