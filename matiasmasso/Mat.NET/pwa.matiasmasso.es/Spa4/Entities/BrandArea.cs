using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Geographical distribution areas agreed with the manufacturer for each product brand
    /// </summary>
    public partial class BrandArea
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Product brand, foreign key for Tpa table
        /// </summary>
        public Guid Brand { get; set; }
        /// <summary>
        /// Foreign key for either Country, Zona or Location
        /// </summary>
        public Guid Area { get; set; }
        /// <summary>
        /// Date of the agreement
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Date of termination
        /// </summary>
        public DateTime? FchTo { get; set; }

        public virtual Tpa BrandNavigation { get; set; } = null!;
    }
}
