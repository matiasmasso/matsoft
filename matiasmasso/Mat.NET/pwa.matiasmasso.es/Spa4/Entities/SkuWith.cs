using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product skus that should also be added to the order when certain products are ordered
    /// </summary>
    public partial class SkuWith
    {
        /// <summary>
        /// Parent product sku that requires other products to be added to the purchase ; foreifgn key for Art table
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Child product sku that is required to be added when parent is ordered; foreifgn key for Art table
        /// </summary>
        public Guid Child { get; set; }
        /// <summary>
        /// Units of child product required for each parent product
        /// </summary>
        public int Qty { get; set; }

        public virtual Art ChildNavigation { get; set; } = null!;
        public virtual Art ParentNavigation { get; set; } = null!;
    }
}
