using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Logs when automated email has been sent to a subscriber
    /// </summary>
    public partial class MailingLog
    {
        /// <summary>
        /// Primary key; foreign key to different tables depending on the subscription. It may be for example Csb table for notifying payments close to due date
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Recipient destination; foreign key for Email table
        /// </summary>
        public Guid Usuari { get; set; }
        /// <summary>
        /// Date ant¡d time the email was sent
        /// </summary>
        public DateTime Fch { get; set; }

        public virtual Email UsuariNavigation { get; set; } = null!;
    }
}
