using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Web visitor contact messages
    /// </summary>
    public partial class ContactMessage
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// User name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// User location, free text
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// Message text
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Date the message was created
        /// </summary>
        public DateTime FchCreated { get; set; }
    }
}
