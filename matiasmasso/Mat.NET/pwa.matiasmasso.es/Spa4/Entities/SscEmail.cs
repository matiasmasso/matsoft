using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Subscriptors subscribed to subscriptions from Ssc table
    /// </summary>
    public partial class SscEmail
    {
        /// <summary>
        /// Subscription; foreign key for Ssc table
        /// </summary>
        public Guid SscGuid { get; set; }
        /// <summary>
        /// User; foreign key for Email table
        /// </summary>
        public Guid Email { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Email EmailNavigation { get; set; } = null!;
        public virtual Ssc SscGu { get; set; } = null!;
    }
}
