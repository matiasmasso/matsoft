using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// We call bundle to a product agrupation composed of 2 or more sku product components. This table lists which components define each bundle.
    /// </summary>
    public partial class SkuBundle
    {
        /// <summary>
        /// Primary key; foreign key for Art table
        /// </summary>
        public Guid Bundle { get; set; }
        /// <summary>
        /// Product sku; foreign key for Art table
        /// </summary>
        public Guid Sku { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Units of this product sku included in the bundle
        /// </summary>
        public int Qty { get; set; }

        public virtual Art BundleNavigation { get; set; } = null!;
        public virtual Art SkuNavigation { get; set; } = null!;
    }
}
