using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Consumer baskets reported by Wtbol affiliates
/// </summary>
public partial class WtbolBasket
{
    public Guid Guid { get; set; }

    public DateTime Fch { get; set; }

    public Guid? Site { get; set; }

    public virtual WtbolSite? SiteNavigation { get; set; }

    public virtual ICollection<WtbolBasketItem> WtbolBasketItems { get; set; } = new List<WtbolBasketItem>();
}
