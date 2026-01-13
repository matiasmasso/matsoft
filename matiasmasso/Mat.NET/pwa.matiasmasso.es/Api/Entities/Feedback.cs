using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Users feedback events log
    /// </summary>
    public partial class Feedback
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to the feedback source object; maybe for example a raffle
        /// </summary>
        public Guid Target { get; set; }
        /// <summary>
        /// Date and time of the event
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Enumerable (1.User, 2.Customer)
        /// </summary>
        public int UserOrCustomerCod { get; set; }
        /// <summary>
        /// Foreign key  to either Email or CliGral depending on UserOrCustomerCod field value
        /// </summary>
        public Guid? UserOrCustomer { get; set; }
        /// <summary>
        /// Feedback event type (1.Like, 2.Share)
        /// </summary>
        public int Cod { get; set; }
    }
}
