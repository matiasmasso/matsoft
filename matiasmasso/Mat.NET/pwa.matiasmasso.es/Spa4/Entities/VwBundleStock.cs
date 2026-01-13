using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Stocks available for product bundles
    /// </summary>
    public partial class VwBundleStock
    {
        public Guid Bundle { get; set; }
        public Guid? MgzGuid { get; set; }
        public int? Expr1 { get; set; }
    }
}
