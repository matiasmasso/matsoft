using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Most recent retail price registered for each product
    /// </summary>
    public partial class VwLastRetailPrice
    {
        public Guid PriceList { get; set; }
        public Guid Art { get; set; }
        public DateTime Fch { get; set; }
        public decimal? Retail { get; set; }
    }
}
