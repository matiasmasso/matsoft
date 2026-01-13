using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Complete product retail price list, including product bundles
    /// </summary>
    public partial class VwRetail
    {
        public Guid SkuGuid { get; set; }
        public decimal? Retail { get; set; }
    }
}
