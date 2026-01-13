using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Retail price list
    /// </summary>
    public partial class VwSkuRetail
    {
        public Guid SkuGuid { get; set; }
        public decimal? Retail { get; set; }
    }
}
