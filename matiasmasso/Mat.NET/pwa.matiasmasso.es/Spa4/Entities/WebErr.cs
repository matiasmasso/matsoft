using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Error events on website
    /// </summary>
    public partial class WebErr
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DTOWebErr.Cods
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Url page causing the error
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// Where did the user linked to this page from
        /// </summary>
        public string? Referrer { get; set; }
        /// <summary>
        /// User, if any. Foreign key for Email table
        /// </summary>
        public Guid? Usr { get; set; }
        /// <summary>
        /// User Ip address
        /// </summary>
        public string? Ip { get; set; }
        /// <summary>
        /// User browser
        /// </summary>
        public string? Browser { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Msg { get; set; }

        public virtual Email? UsrNavigation { get; set; }
    }
}
