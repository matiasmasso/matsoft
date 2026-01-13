using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact documentation
    /// </summary>
    public partial class CliDoc
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Document owner. Foreign key for CliGral table
        /// </summary>
        public Guid Contact { get; set; }
        /// <summary>
        /// Enumerable DTOContactDoc.Types (1.Nif, 2.Deed...)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Document date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Document title
        /// </summary>
        public string Ref { get; set; } = null!;
        /// <summary>
        /// True if outdated
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Pdf document. Foreign key to Docfile table
        /// </summary>
        public string? Hash { get; set; }

        public virtual CliGral ContactNavigation { get; set; } = null!;
        public virtual DocFile? HashNavigation { get; set; }
    }
}
