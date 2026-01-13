using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Log of Json messages received in our Api  https://matiasmasso-api.azurewebsites.net/api/JsonLog/mailbox
    /// </summary>
    public partial class JsonLog
    {
        public JsonLog()
        {
            DeliveryShipments = new HashSet<DeliveryShipment>();
            DeliveryTrackings = new HashSet<DeliveryTracking>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date the message was logged
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Message Json code
        /// </summary>
        public string Json { get; set; } = null!;
        /// <summary>
        /// Schema the message conforms to
        /// </summary>
        public Guid? Schema { get; set; }
        /// <summary>
        /// Enumerable DTOJsonLog.Results (1.Success, 2.Failure)
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// Primary key to a relevant table for the message object (for example, in case of a customer sending a purchase order, the value would be the foreign key for Pdc table)
        /// </summary>
        public Guid? ResultTarget { get; set; }

        public virtual JsonSchema? SchemaNavigation { get; set; }
        public virtual ICollection<DeliveryShipment> DeliveryShipments { get; set; }
        public virtual ICollection<DeliveryTracking> DeliveryTrackings { get; set; }
    }
}
