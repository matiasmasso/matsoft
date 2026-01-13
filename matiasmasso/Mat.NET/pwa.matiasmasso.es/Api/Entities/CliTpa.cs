using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer product ranges
    /// </summary>
    public partial class CliTpa
    {
        /// <summary>
        /// Customer; foreign key for CliGral table
        /// </summary>
        public Guid CliGuid { get; set; }
        /// <summary>
        /// Product range; it may either be a brand, a brand category or a sku, foreign key to Tpa, Stp or Art tables
        /// </summary>
        public Guid ProductGuid { get; set; }
        /// <summary>
        /// Inclusion or exclusion; enumerable DTOCliProductBlocked.Codis
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Postal code in case it takes area exclusivity
        /// </summary>
        public string Zip { get; set; } = null!;
        /// <summary>
        /// Comments justifying range inclusion or exclusion
        /// </summary>
        public string Obs { get; set; } = null!;

        public virtual CliGral CliGu { get; set; } = null!;
    }
}
