using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Each time a visitor browsers info from one of our products, a list of limited recommended online retailers is displayed so the consumer can quickly purchase it. These online retailers are affiliated to our WTBOL project (Where to buy Online) and criteria to be displayed depends on whether the retailer has registered a landing page for this product, has declared stock availability in the last 24 hours, and which is his ranking on CTR.
    /// This table registers each time the retailer is displayed so we can calculate the CTR
    /// </summary>
    public partial class WtbolSerp
    {
        public WtbolSerp()
        {
            WtbolSerpItems = new HashSet<WtbolSerpItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// User session; foreign key for UsrSession
        /// </summary>
        public Guid? Session { get; set; }
        /// <summary>
        /// Date and time
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Product the consumer was interested in
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Website visitor Ip
        /// </summary>
        public string? Ip { get; set; }
        /// <summary>
        /// Website visitor ISO 3166-1 country code
        /// </summary>
        public string? CountryCode { get; set; }
        /// <summary>
        /// Website visitor browser details
        /// </summary>
        public string? UserAgent { get; set; }

        public virtual ICollection<WtbolSerpItem> WtbolSerpItems { get; set; }
    }
}
