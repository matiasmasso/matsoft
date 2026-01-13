using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Logs each time a visitor uses our website search box, registering the search key used
    /// </summary>
    public partial class SearchRequest
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Text typed by the visitor on the search box
        /// </summary>
        public string SearchKey { get; set; } = null!;
        /// <summary>
        /// Date and time of the request
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// User, if logged in. Foreign key for Email table
        /// </summary>
        public Guid? Email { get; set; }

        public virtual Email? EmailNavigation { get; set; }
    }
}
