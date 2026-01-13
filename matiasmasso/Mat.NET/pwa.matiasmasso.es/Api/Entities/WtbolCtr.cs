using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Each time a visitor on our website selects an online retailer to purchase the product he was browsing, we log the event in this table so we can calculate the Ctr and build a retailer ranking to prioritize display order
    /// </summary>
    public partial class WtbolCtr
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date and time the visitor clicked the affiliate link
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Product he was browsing; foreign key for Art table
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Affiliate site; foreign key for Wtbol site
        /// </summary>
        public Guid Site { get; set; }
        /// <summary>
        /// Visitor Ip
        /// </summary>
        public string? Ip { get; set; }

        public virtual WtbolSite SiteNavigation { get; set; } = null!;
    }
}
