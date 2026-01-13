using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Website url redirections
    /// </summary>
    public partial class WebPageAlias
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Old url to be redirected
        /// </summary>
        public string UrlFrom { get; set; } = null!;
        /// <summary>
        /// New landing page to display when the old url is requested
        /// </summary>
        public string UrlTo { get; set; } = null!;
        /// <summary>
        /// Enumerable (0.all domains, 1.only es domain, 2.omly pt domain)
        /// </summary>
        public int Domain { get; set; }
    }
}
