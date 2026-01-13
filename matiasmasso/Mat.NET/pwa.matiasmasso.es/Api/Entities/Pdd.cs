using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Shortcuts for purchase order concepts
    /// </summary>
    public partial class Pdd
    {
        public Guid Guid { get; set; }
        public string? SearchKey { get; set; }
        /// <summary>
        /// Purchase order source. Enumerable DTOPurchaseOrder.Sources
        /// </summary>
        public short Src { get; set; }
        /// <summary>
        /// Shortcut. A short text the user enters which the system converts into an often used text
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Concept in Spanish language
        /// </summary>
        public string Esp { get; set; } = null!;
        /// <summary>
        /// Concept in Catalan language
        /// </summary>
        public string Cat { get; set; } = null!;
        /// <summary>
        /// Concept in English language
        /// </summary>
        public string Eng { get; set; } = null!;
    }
}
