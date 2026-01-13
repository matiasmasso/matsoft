using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product cost from supplier current price list
    /// </summary>
    public partial class VwSkuCost
    {
        public Guid SkuGuid { get; set; }
        public decimal Price { get; set; }
        public string Cur { get; set; } = null!;
        public decimal DiscountOnInvoice { get; set; }
        public decimal DiscountOffInvoice { get; set; }
        public Guid Proveidor { get; set; }
        public Guid PriceListGuid { get; set; }
        public DateTime PriceListFch { get; set; }
        public string? Hash { get; set; }
    }
}
