using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Current price list date for each product
    /// </summary>
    public partial class VwSkuLastCost
    {
        public Guid SkuGuid { get; set; }
        public string SkuRef { get; set; } = null!;
        public Guid Proveidor { get; set; }
        public DateTime? LastFch { get; set; }
    }
}
