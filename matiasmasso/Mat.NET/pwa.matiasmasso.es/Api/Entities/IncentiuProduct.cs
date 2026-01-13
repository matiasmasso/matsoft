using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product range where a promotion is valid
    /// </summary>
    public partial class IncentiuProduct
    {
        /// <summary>
        /// Foreign key for parent table Incentiu
        /// </summary>
        public Guid Incentiu { get; set; }
        /// <summary>
        /// Product range; foreign key for either product brand table Tpa, product category table Stp or product sku table Art
        /// </summary>
        public Guid Product { get; set; }

        public virtual Incentiu IncentiuNavigation { get; set; } = null!;
    }
}
