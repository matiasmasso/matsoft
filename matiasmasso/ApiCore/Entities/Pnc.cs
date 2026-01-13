using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Purchase order items
/// </summary>
public partial class Pnc
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to parent table Pdc
    /// </summary>
    public Guid PdcGuid { get; set; }

    /// <summary>
    /// Internal line number
    /// </summary>
    public int Lin { get; set; }

    /// <summary>
    /// Line number matching original customer purchase order
    /// </summary>
    public int? CustomLin { get; set; }

    /// <summary>
    /// Product Sku. Foreign key to table Art
    /// </summary>
    public Guid ArtGuid { get; set; }

    /// <summary>
    /// Units ordered
    /// </summary>
    public int Qty { get; set; }

    /// <summary>
    /// Ordered units not delivered yet
    /// </summary>
    public int Pn2 { get; set; }

    /// <summary>
    /// Price in foreign currency
    /// </summary>
    public decimal Pts { get; set; }

    /// <summary>
    /// Currency as ISO 4217
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Price in Eur
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Discount on price, if any
    /// </summary>
    public decimal Dto { get; set; }

    public decimal Dt2 { get; set; }

    /// <summary>
    /// if false, free of charge
    /// </summary>
    public bool Carrec { get; set; }

    /// <summary>
    /// Commercial agent when commission applies. Foreign key for CliRep table
    /// </summary>
    public Guid? RepGuid { get; set; }

    /// <summary>
    /// Commission percentage for the commercial agent
    /// </summary>
    public decimal? Com { get; set; }

    /// <summary>
    /// Estimated Time of Delivery (ETD)
    /// </summary>
    public DateTime? FchConfirm { get; set; }

    /// <summary>
    /// Sometimes there is the need to assign a commission to a rep different than the default one. Setting this value to true prevents the system from overriding the rep when validating the order
    /// </summary>
    public bool? RepCustom { get; set; }

    /// <summary>
    /// Free text to explain why we set a custom rep rather than d¡the default one for this order
    /// </summary>
    public string? Custom { get; set; }

    /// <summary>
    /// A bundle is a virtual product sku composed by an agregation of different product skus that are sold all together.
    /// In order to identify it on an order, this field takes the same value of the primary key of the bundle purchase order item, which means it should be displayed but it does not affect inventory.
    /// The following items list the components of the bundle and keep the parent value on the bundle field.
    /// This means they should be display as components included in the bundle and they affect the inventory
    /// </summary>
    public Guid? Bundle { get; set; }

    /// <summary>
    /// Enumerated in DTOPurchaseOrder.Errcodes
    /// </summary>
    public int ErrCod { get; set; }

    /// <summary>
    /// Error description
    /// </summary>
    public string? ErrDsc { get; set; }

    public virtual Art Art { get; set; } = null!;

    public virtual Pnc? BundleNavigation { get; set; }

    public virtual ICollection<Pnc> InverseBundleNavigation { get; set; } = new List<Pnc>();

    public virtual Pdc Pdc { get; set; } = null!;
}
