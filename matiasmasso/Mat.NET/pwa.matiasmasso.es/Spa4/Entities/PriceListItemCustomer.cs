using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Retail prices
    /// </summary>
    public partial class PriceListItemCustomer
    {
        /// <summary>
        /// Foreign key to parent table PriceList_Customer
        /// </summary>
        public Guid PriceList { get; set; }
        /// <summary>
        /// Product; foreign key to Art table
        /// </summary>
        public Guid Art { get; set; }
        /// <summary>
        /// If Customer field value is null, retail price. Other else, the value is the specific cost for this customer
        /// </summary>
        public decimal? Retail { get; set; }

        public virtual Art ArtNavigation { get; set; } = null!;
        public virtual PriceListCustomer PriceListNavigation { get; set; } = null!;
    }
}
