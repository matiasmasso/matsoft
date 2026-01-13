using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class ShoppingBasketItem
{
    public Guid Parent { get; set; }

    public int Lin { get; set; }

    public Guid Sku { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public decimal Dto { get; set; }

    public virtual ShoppingBasket ParentNavigation { get; set; } = null!;

    public virtual Art SkuNavigation { get; set; } = null!;
}
