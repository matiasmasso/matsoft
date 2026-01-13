using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Specific products may be subjected to minimum quantities per order. In certain cases, it may be needed to skip this constraint during a short time to allow an authorised user to enter an order with a single unit, for example to attend a warranty reposition. This teble keeps track of such requests to revert the operation after a couple of minutes
/// </summary>
public partial class SkuMoqLock
{
    /// <summary>
    /// Product requested to release minimum order quantity. Foreign key for Art table
    /// </summary>
    public Guid Sku { get; set; }

    /// <summary>
    /// User requesting to release product MOQ (Minimum Order Quantity). Foreingn key to Email table
    /// </summary>
    public Guid Usr { get; set; }

    /// <summary>
    /// Date and time the user requests to release the product minimum order quantity- It will only be released a couple of minutes, this is why the time is registered.
    /// </summary>
    public DateTime Fch { get; set; }

    public virtual Art SkuNavigation { get; set; } = null!;

    public virtual Email UsrNavigation { get; set; } = null!;
}
