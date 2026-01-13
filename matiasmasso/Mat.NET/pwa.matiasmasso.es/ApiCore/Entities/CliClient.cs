using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Customers properties
/// </summary>
public partial class CliClient
{
    /// <summary>
    /// Primary key; foreign key for CliGral table
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Headquarters to invoice sales delivered to this destination, in case not self. Foreign key for CliGral table
    /// </summary>
    public Guid? CcxGuid { get; set; }

    /// <summary>
    /// Customer reference to distinguish this destination among others from same customer
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// True if the customer wants to centralize all orders to headquarters
    /// </summary>
    public byte OrdersToCentral { get; set; }

    /// <summary>
    /// Global discount agreed, if any
    /// </summary>
    public decimal Dto { get; set; }

    /// <summary>
    /// False if VAT exempt
    /// </summary>
    public bool Iva { get; set; }

    /// <summary>
    /// False if Equivalence tax exempt
    /// </summary>
    public bool Req { get; set; }

    /// <summary>
    /// Message to display to operators when entering orders or deliveries to this customer
    /// </summary>
    public string? Warning { get; set; }

    /// <summary>
    /// Comments like opening hours, etc
    /// </summary>
    public string? ObsComercial { get; set; }

    /// <summary>
    /// If false, prices are hidden from delivery notes
    /// </summary>
    public bool AlbVal { get; set; }

    /// <summary>
    /// DTOCustomer.CashCodes (1.credit, 2.cash against goods, 2.previous transfer...)
    /// </summary>
    public byte CashCod { get; set; }

    /// <summary>
    /// DTOCustomer.PortsCodes (1.paid on origin, 2.paid on destination...)
    /// </summary>
    public byte Ports { get; set; }

    /// <summary>
    /// Our supplier number on customers database
    /// </summary>
    public string? SuProveedorNum { get; set; }

    /// <summary>
    /// If true, no rep gets any commission
    /// </summary>
    public bool NoRep { get; set; }

    /// <summary>
    /// If true, customer should not be displayed as a sale point to consumers
    /// </summary>
    public bool NoWeb { get; set; }

    /// <summary>
    /// If true, no promos are available to this customer
    /// </summary>
    public bool NoIncentius { get; set; }

    /// <summary>
    /// If true, this customer should be removed from sale points participants list on raffles
    /// </summary>
    public bool NoRaffles { get; set; }

    /// <summary>
    /// If true, this destination has specific payment terms which may differ from other destinations of same customer
    /// </summary>
    public bool FpgIndependent { get; set; }

    /// <summary>
    /// If any, deliveries to this customer should be addressed to the platform. Foreign key for CliGral table
    /// </summary>
    public Guid? DeliveryPlatform { get; set; }

    /// <summary>
    /// If true, invoice items should include product EAN numbers
    /// </summary>
    public bool MostrarEanenFactura { get; set; }

    /// <summary>
    /// Rate of online sales within this customer turnover
    /// </summary>
    public int QuotaOnline { get; set; }

    /// <summary>
    /// Invoices delivery mode; Enumerable DTOCustomer.FraPrintModes (1.No print, 2.Paper, 3.Email, 4.Edi)
    /// </summary>
    public short FraPrintMode { get; set; }

    /// <summary>
    /// Payment way code DTOPaymentTerms.CodsFormaDePago
    /// </summary>
    public int Cfp { get; set; }

    /// <summary>
    /// Number of months to pay after invoice date
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// If any, days when the customer pays after the months from previous field
    /// </summary>
    public string? PaymentDays { get; set; }

    /// <summary>
    /// Holidays which imply payments delay. 6 comma separated digits in 3 groups of month,day: from, to, forward to
    /// </summary>
    public string? Vacaciones { get; set; }

    /// <summary>
    /// Time window for deliveries reception
    /// </summary>
    public string? HorarioEntregas { get; set; }

    /// <summary>
    /// Enumerable DTOInvoice.ExportCods (1.national, 2.UE, 3.rest of the world)
    /// </summary>
    public int ExportCod { get; set; }

    /// <summary>
    /// International Commerce Terms; foreign key for Incoterm table
    /// </summary>
    public string? Incoterm { get; set; }

    /// <summary>
    /// Priority when displaying sale points to consumers
    /// </summary>
    public int WebAtlasPriority { get; set; }

    /// <summary>
    /// In case this customer is part of a holding corporation, foreign key for Holding table
    /// </summary>
    public Guid? Holding { get; set; }

    /// <summary>
    /// For sale points from a unique customer, cluster classification. Foreign key for CustomerClustyer
    /// </summary>
    public Guid? CustomerCluster { get; set; }

    /// <summary>
    /// Foreign key for PortsCondicions table
    /// </summary>
    public Guid? PortsCondicions { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    public virtual CliClient? Ccx { get; set; }

    public virtual CliGral Gu { get; set; } = null!;

    public virtual Holding? HoldingNavigation { get; set; }

    public virtual Incoterm? IncotermNavigation { get; set; }

    public virtual ICollection<CliClient> InverseCcx { get; set; } = new List<CliClient>();

    public virtual ICollection<RepCliCom> RepCliComs { get; set; } = new List<RepCliCom>();
}
