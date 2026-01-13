using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Prevents user from concurrently registering orders or delivery notes for same customer
    /// </summary>
    public partial class AlbBloqueig
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date and time of the lock request
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// PDC for purchase order, ALB for delivery notes
        /// </summary>
        public string Cod { get; set; } = null!;
        /// <summary>
        /// User requesting lock. Foreign key to Email table
        /// </summary>
        public Guid? UserGuid { get; set; }
        /// <summary>
        /// Contact being locked. Foreign key for CliGral table
        /// </summary>
        public Guid? Contact { get; set; }

        public virtual CliGral? ContactNavigation { get; set; }
        public virtual Email? UserGu { get; set; }
    }
}
