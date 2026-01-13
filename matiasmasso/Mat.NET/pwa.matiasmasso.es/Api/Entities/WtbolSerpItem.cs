using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Affiliated online commerces and the position they are displayed when recommended to our website visitors
    /// </summary>
    public partial class WtbolSerpItem
    {
        /// <summary>
        /// Foreign key for parent table WtbolSerp
        /// </summary>
        public Guid Serp { get; set; }
        /// <summary>
        /// Order in which the affiliated site is displayed
        /// </summary>
        public int Pos { get; set; }
        /// <summary>
        /// Affiliated site offered to our website visitor
        /// </summary>
        public Guid Site { get; set; }

        public virtual WtbolSerp SerpNavigation { get; set; } = null!;
        public virtual WtbolSite SiteNavigation { get; set; } = null!;
    }
}
