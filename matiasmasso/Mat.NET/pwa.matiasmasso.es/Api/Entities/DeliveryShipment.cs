using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Log provided by our logistic center reporting delivery packages contents
    /// </summary>
    public partial class DeliveryShipment
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key to Emp table
        /// </summary>
        public int? Emp { get; set; }
        /// <summary>
        /// Message received. Foreign key to JsonLog table
        /// </summary>
        public Guid? Log { get; set; }
        /// <summary>
        /// VAT number of logistic center
        /// </summary>
        public string? Sender { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Logistic center expedition number
        /// </summary>
        public string? Expedition { get; set; }
        /// <summary>
        /// Our delivery number, formated YYYYNNNNNN
        /// </summary>
        public string? Delivery { get; set; }
        /// <summary>
        /// Expedition total of number of packages
        /// </summary>
        public int? Packages { get; set; }
        /// <summary>
        /// Pallet plate number, if any
        /// </summary>
        public string? Pallet { get; set; }
        /// <summary>
        /// Package number
        /// </summary>
        public int? Package { get; set; }
        /// <summary>
        /// Package plate number
        /// </summary>
        public string? Sscc { get; set; }
        /// <summary>
        /// Product Sku; foreign key for Art table
        /// </summary>
        public int? Sku { get; set; }
        /// <summary>
        /// Units of this product inside this package
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// Delivery line number
        /// </summary>
        public int? Line { get; set; }

        public virtual JsonLog? LogNavigation { get; set; }
    }
}
