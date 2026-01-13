using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customers per agent and start date
    /// </summary>
    public partial class VwRepCustomer
    {
        /// <summary>
        /// Foreign key to CliGral table
        /// </summary>
        public Guid Customer { get; set; }
        /// <summary>
        /// Foreign key to CliGral table
        /// </summary>
        public Guid Rep { get; set; }
        /// <summary>
        /// Date the customer (indeed his area and channel) was assigned to this agent
        /// </summary>
        public DateTime? FchFrom { get; set; }
    }
}
