using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product bundle retail price list
    /// </summary>
    public partial class VwSkuBundleRetail
    {
        public Guid SkuGuid { get; set; }
        public decimal? Retail { get; set; }
    }
}
