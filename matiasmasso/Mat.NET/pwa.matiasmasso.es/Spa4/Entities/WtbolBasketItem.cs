using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Consumer basket items
    /// </summary>
    public partial class WtbolBasketItem
    {
        public Guid Basket { get; set; }
        public int Lin { get; set; }
        public Guid Sku { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public virtual WtbolBasket BasketNavigation { get; set; } = null!;
        public virtual Art SkuNavigation { get; set; } = null!;
    }
}
