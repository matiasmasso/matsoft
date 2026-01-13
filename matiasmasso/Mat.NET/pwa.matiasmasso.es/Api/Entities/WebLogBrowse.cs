using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Logs when visitor browses content on the website like bloog or news
    /// </summary>
    public partial class WebLogBrowse
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Target browsed; foreign key to LangText table
        /// </summary>
        public Guid Doc { get; set; }
        /// <summary>
        /// Date and time of the event
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Visitor, if logged in; foreign key for Email table
        /// </summary>
        public Guid? User { get; set; }

        public virtual Email? UserNavigation { get; set; }
    }
}
