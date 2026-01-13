using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product url segments
    /// </summary>
    public partial class UrlSegment
    {
        /// <summary>
        /// Product; foreign key to either brand Tpa table, category Stp table, sku Art table
        /// </summary>
        public Guid Target { get; set; }
        /// <summary>
        /// Url segment
        /// </summary>
        public string Segment { get; set; } = null!;
        /// <summary>
        /// ISO 639-2 language code (3 letters)
        /// </summary>
        public string? Lang { get; set; }
        /// <summary>
        /// True if this is the right segment for canonic Url
        /// </summary>
        public bool Canonical { get; set; }
    }
}
