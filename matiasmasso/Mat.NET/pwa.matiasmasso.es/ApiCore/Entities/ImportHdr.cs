using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Import consignments
/// </summary>
public partial class ImportHdr
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company; foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Import year
    /// </summary>
    public int Yea { get; set; }

    /// <summary>
    /// Import Id, sequential number unique for each Company/Year combination
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Supplier shipping the imported goods
    /// </summary>
    public Guid? PrvGuid { get; set; }

    /// <summary>
    /// International transport
    /// </summary>
    public Guid? TrpGuid { get; set; }

    /// <summary>
    /// Number of packages
    /// </summary>
    public int Bultos { get; set; }

    /// <summary>
    /// Load weigh, in Kg
    /// </summary>
    public decimal Kg { get; set; }

    /// <summary>
    /// Load volume, in m3
    /// </summary>
    public decimal M3 { get; set; }

    /// <summary>
    /// Date of delivery
    /// </summary>
    public DateTime Fch { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// Goods value, in foreign currency
    /// </summary>
    public decimal Val { get; set; }

    /// <summary>
    /// ISO 4217 currency code
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Goods value, in Euros
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// International Commerce Terms; foreign key for Incoterm table
    /// </summary>
    public string? IncoTerms { get; set; }

    /// <summary>
    /// ISO 3166-1 country code
    /// </summary>
    public string PaisOrigen { get; set; } = null!;

    /// <summary>
    /// Week number within the year, starting the first week with at least 4 days inside the new year
    /// </summary>
    public int Week { get; set; }

    /// <summary>
    /// True if the goods have already arrived to our warehouse
    /// </summary>
    public bool Arrived { get; set; }

    /// <summary>
    /// Truck plate or vessel number
    /// </summary>
    public string? Matricula { get; set; }

    /// <summary>
    /// Estimated delivery time
    /// </summary>
    public DateTime? FchEtd { get; set; }

    /// <summary>
    /// Date the transport company has been notified in order to schedule the truck or vessel
    /// </summary>
    public DateTime? FchAvisTrp { get; set; }

    /// <summary>
    /// Platform where the goods have to be picked up, if different from the supplier main address; foreign key for CliGral table
    /// </summary>
    public Guid? PlataformaDeCarga { get; set; }

    public bool Disabled { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<ImportDtl> ImportDtls { get; set; } = new List<ImportDtl>();

    public virtual ICollection<ImportPrevisio> ImportPrevisios { get; set; } = new List<ImportPrevisio>();

    public virtual ICollection<InvoiceReceivedHeader> InvoiceReceivedHeaders { get; set; } = new List<InvoiceReceivedHeader>();

    public virtual CliGral? PlataformaDeCargaNavigation { get; set; }

    public virtual CliGral? Prv { get; set; }

    public virtual CliGral? Trp { get; set; }
}
