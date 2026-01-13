using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Stocks declared daily by affiliated online retailers
    /// </summary>
    public partial class WtbolStock
    {
        /// <summary>
        /// Affiliated site; foreign key for WtbolSite table
        /// </summary>
        public Guid Site { get; set; }
        /// <summary>
        /// Product; foreign key for Art table
        /// </summary>
        public Guid Sku { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Stock available the affiliate declares 
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// Retail price he is offering the product for
        /// </summary>
        public decimal? Price { get; set; }

        public virtual WtbolSite SiteNavigation { get; set; } = null!;
        public virtual Art SkuNavigation { get; set; } = null!;
    }
}
