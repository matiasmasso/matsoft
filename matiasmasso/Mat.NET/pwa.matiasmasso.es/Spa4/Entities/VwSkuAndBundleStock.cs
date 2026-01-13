using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Stocks, including product bundles
    /// </summary>
    public partial class VwSkuAndBundleStock
    {
        public Guid SkuGuid { get; set; }
        public Guid? MgzGuid { get; set; }
        public int? Stock { get; set; }
    }
}
