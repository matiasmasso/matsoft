using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Agent portfolio
    /// </summary>
    public partial class RepProduct
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Agent. Foreign key to CliGral table
        /// </summary>
        public Guid Rep { get; set; }
        /// <summary>
        /// Product. May be a brand, category or sku
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Working area where agent customers are based
        /// </summary>
        public Guid Area { get; set; }
        /// <summary>
        /// Distribution channels granted to this agent within his working area
        /// </summary>
        public Guid DistributionChannel { get; set; }
        /// <summary>
        /// Enumerable DTORepProduct.Cods: 0.NotSet, 1.Included, 2.Excluded
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Date of agreement
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Date of termination
        /// </summary>
        public DateTime? FchTo { get; set; }
        /// <summary>
        /// Standard commission for sales to customers of agreed channels within agreed areas
        /// </summary>
        public decimal? ComStd { get; set; }
        /// <summary>
        /// Reduced commission when agreed for specific operations
        /// </summary>
        public decimal? ComRed { get; set; }
        /// <summary>
        /// Entry creation date
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual DistributionChannel DistributionChannelNavigation { get; set; } = null!;
        public virtual CliRep RepNavigation { get; set; } = null!;
    }
}
