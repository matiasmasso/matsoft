using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Retail price for product packs (bundles) of different components
    /// </summary>
    public partial class VwBundleRetail
    {
        public Guid Bundle { get; set; }
        public decimal? Retail { get; set; }
    }
}
