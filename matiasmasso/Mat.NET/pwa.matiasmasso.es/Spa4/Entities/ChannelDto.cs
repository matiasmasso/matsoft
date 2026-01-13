using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Default discount over retail price for new customers from each distributioon channel. Used to generate price list
    /// </summary>
    public partial class ChannelDto
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Distribution channel. Foreign key for DistributionChannel table
        /// </summary>
        public Guid Channel { get; set; }
        /// <summary>
        /// Foreign key for either product brand Tpa table, product category Stp table or product sku Art table
        /// </summary>
        public Guid? Product { get; set; }
        /// <summary>
        /// Discount on retail price to calculate price list
        /// </summary>
        public decimal Dto { get; set; }
        /// <summary>
        /// Date this discount was set. Only the most recent entry for a channel/product combination is used to calculate the price list
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }

        public virtual DistributionChannel ChannelNavigation { get; set; } = null!;
    }
}
