using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Purchase Order header details
/// </summary>
public partial class Pdc
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company Id. Foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Year where the order number belongs to (each year order numbers start from zero)
    /// </summary>
    public short Yea { get; set; }

    /// <summary>
    /// Internal order number within same company and year
    /// </summary>
    public int Pdc1 { get; set; }

    /// <summary>
    /// Enuimeration DTOPurchaseOrder.Codis: 1.To Suplier, 2.From Customer...
    /// </summary>
    public short Cod { get; set; }

    /// <summary>
    /// Customer (or supplier). Foreign key for CliGral table.
    /// </summary>
    public Guid CliGuid { get; set; }

    /// <summary>
    /// Platform where to deliver this order. Foreign key to CliGral
    /// </summary>
    public Guid? Platform { get; set; }

    /// <summary>
    /// Account where to invoice this delivery, if different from default. Foreign key to CliGral table
    /// </summary>
    public Guid? FacturarA { get; set; }

    /// <summary>
    /// Promotion applied, if any. Foreign key to Incentius table
    /// </summary>
    public Guid? Promo { get; set; }

    /// <summary>
    /// Customer order number or any text the customer may need to identify this order
    /// </summary>
    public string Pdd { get; set; } = null!;

    /// <summary>
    /// Where did this order came from. Enumeration DTOPurchaseOrder.Sources
    /// </summary>
    public short Src { get; set; }

    /// <summary>
    /// International Commerce Terms
    /// </summary>
    public string? Incoterm { get; set; }

    /// <summary>
    /// Official order date
    /// </summary>
    public DateOnly Fch { get; set; }

    /// <summary>
    /// Don&apos;t deliver before this date, if any
    /// </summary>
    public DateOnly? FchMin { get; set; }

    /// <summary>
    /// Don&apos;t deliver after this date, if any
    /// </summary>
    public DateOnly? FchMax { get; set; }

    /// <summary>
    /// If true, the order should not be fractionated in different deliveries
    /// </summary>
    public bool TotJunt { get; set; }

    /// <summary>
    /// If true, customer does not want it to be delivered yet and goods may be sold to alternative customers
    /// </summary>
    public byte Pot { get; set; }

    /// <summary>
    /// If true, stock should be reserved to make sure we can deliver on the right date
    /// </summary>
    public bool BlockStock { get; set; }

    /// <summary>
    /// If true, no commission should be granted to any agent for this order
    /// </summary>
    public bool NoRep { get; set; }

    /// <summary>
    /// if true, this order should not be displayed to customer/supplier
    /// </summary>
    public bool Hide { get; set; }

    /// <summary>
    /// Global discount for the whole order
    /// </summary>
    public float Dto { get; set; }

    /// <summary>
    /// Second level of discount for the whole order
    /// </summary>
    public float Dt2 { get; set; }

    /// <summary>
    /// Discount for early payment
    /// </summary>
    public float Dpp { get; set; }

    /// <summary>
    /// Amount in foreign currency
    /// </summary>
    public decimal PdcPts { get; set; }

    /// <summary>
    /// Order amount in Euro currency
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Currency (ISO 4217)
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Payment terms if different from customer default. XML or JSON coded
    /// </summary>
    public string? Fpg { get; set; }

    /// <summary>
    /// Comments, customer special instructions
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// A link for the warehouse to download customer specific documentation to include on the package, usually an consumer invoice from the e-commerce
    /// </summary>
    public string? CustomerDocUrl { get; set; }

    /// <summary>
    /// Hash of labels to attach to the packaging. Foreign key to DocFile table
    /// </summary>
    public string? EtiquetesTransport { get; set; }

    /// <summary>
    /// Hash of the original Purchase Order Pdf document. Foreign key for Docfile table
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// User who registered the order. Foreign key to Email table
    /// </summary>
    public Guid? UsrCreatedGuid { get; set; }

    /// <summary>
    /// Date and time this order was registered
    /// </summary>
    public DateTime? FchCreated { get; set; }

    /// <summary>
    /// Last user who edited this order. Foreign key to Email table.
    /// </summary>
    public Guid? UsrLastEditedGuid { get; set; }

    /// <summary>
    /// Last date this order was edited
    /// </summary>
    public DateTime? FchLastEdited { get; set; }

    public string? Nadms { get; set; }

    public string? Docfile { get; set; }

    public virtual ICollection<Arc> Arcs { get; set; } = new List<Arc>();

    public virtual CliGral Cli { get; set; } = null!;

    public virtual ICollection<EdiversaDesadvHeader> EdiversaDesadvHeaders { get; set; } = new List<EdiversaDesadvHeader>();

    public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaders { get; set; } = new List<EdiversaOrderHeader>();

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual CliGral? FacturarANavigation { get; set; }

    public virtual ICollection<InvoiceReceivedItem> InvoiceReceivedItems { get; set; } = new List<InvoiceReceivedItem>();

    public virtual CliGral? PlatformNavigation { get; set; }

    public virtual ICollection<Pnc> Pncs { get; set; } = new List<Pnc>();

    public virtual Incentiu? PromoNavigation { get; set; }

    public virtual ICollection<RecallCli> RecallClis { get; set; } = new List<RecallCli>();

    public virtual Email? UsrCreated { get; set; }

    public virtual Email? UsrLastEdited { get; set; }
}
