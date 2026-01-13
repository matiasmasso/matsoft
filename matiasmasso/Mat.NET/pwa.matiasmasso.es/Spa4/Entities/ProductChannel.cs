using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product range per distribution channel
    /// </summary>
    public partial class ProductChannel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Product range; foreign key for either product brand Tpa table, product category Stp table or product sku Art table
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Foreign key for DistributionChannel table
        /// </summary>
        public Guid DistributionChannel { get; set; }
        /// <summary>
        /// Defines wheter the product range is included or excluded from this channel. Enumerable DTOProductChannel.Cods (0.Included, 1.Excluded)
        /// </summary>
        public byte Cod { get; set; }

        public virtual DistributionChannel DistributionChannelNavigation { get; set; } = null!;
    }
}
