using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Products included on a PremiumLine range
    /// </summary>
    public partial class PremiumProduct
    {
        /// <summary>
        /// Foreign key to parent table PremiumLine
        /// </summary>
        public Guid PremiumLine { get; set; }
        /// <summary>
        /// Foreign key to product category Stp table
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Sort order in which this product should be listed
        /// </summary>
        public int Ord { get; set; }

        public virtual PremiumLine PremiumLineNavigation { get; set; } = null!;
        public virtual Stp ProductNavigation { get; set; } = null!;
    }
}
