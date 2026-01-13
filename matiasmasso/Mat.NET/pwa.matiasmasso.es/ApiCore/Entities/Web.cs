using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Distributors per product to be published as recommended sale points. They are published depending on recent purchases of each product and availability of sale points on same area. A Windows service updates this table on a daily base
/// </summary>
public partial class Web
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Product brand; foreign key to Tpa table
    /// </summary>
    public Guid Brand { get; set; }

    /// <summary>
    /// Product department; foreign key for Dept table
    /// </summary>
    public Guid? Dept { get; set; }

    /// <summary>
    /// Product category; foreign key for Stp table
    /// </summary>
    public Guid Category { get; set; }

    /// <summary>
    /// Product Sku; foreign key for Art table
    /// </summary>
    public Guid? Sku { get; set; }

    /// <summary>
    /// Manufacturer product code. Useful for integrations with suppliers so they can also recomend same retailers for same products
    /// </summary>
    public string? SkuRef { get; set; }

    /// <summary>
    /// Supplier; foreign key for CliGral table
    /// </summary>
    public Guid? Proveidor { get; set; }

    /// <summary>
    /// Customer; foreign key for CliGral table
    /// </summary>
    public Guid Client { get; set; }

    /// <summary>
    /// Retailer location; foreign key for Location table
    /// </summary>
    public Guid? Location { get; set; }

    /// <summary>
    /// Account where invoices to this retailer are invoiced, if different from retailer. Used to totalize purchases and divide them among the number of outlets a particular customer owns.
    /// </summary>
    public Guid? Ccx { get; set; }

    /// <summary>
    /// Retailer commerccial name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Retailer sale point address
    /// </summary>
    public string Adr { get; set; } = null!;

    /// <summary>
    /// Retailer sale point location name
    /// </summary>
    public string Cit { get; set; } = null!;

    /// <summary>
    /// Retailer sale point zip code
    /// </summary>
    public string? ZipCod { get; set; }

    /// <summary>
    /// Either the province or the zone; foreign key for Provincia or Zona tables
    /// </summary>
    public Guid? AreaGuid { get; set; }

    /// <summary>
    /// Either the province or the zone name
    /// </summary>
    public string? AreaNom { get; set; }

    /// <summary>
    /// Foreign key for Country table
    /// </summary>
    public Guid? Country { get; set; }

    /// <summary>
    /// retailer sale point phone number
    /// </summary>
    public string Tel { get; set; } = null!;

    /// <summary>
    /// True if the customer has unpaid invoices
    /// </summary>
    public bool Impagat { get; set; }

    /// <summary>
    /// True if this retailer is not convenient to publish to consumers
    /// </summary>
    public bool Blocked { get; set; }

    /// <summary>
    /// True if outdated
    /// </summary>
    public bool Obsoleto { get; set; }

    /// <summary>
    /// If true, it is displayed regardless its turnover
    /// </summary>
    public int? ConsumerPriority { get; set; }

    /// <summary>
    /// Number of outlets
    /// </summary>
    public int SalePointsCount { get; set; }

    /// <summary>
    /// Turnover for the last X months
    /// </summary>
    public decimal Val { get; set; }

    /// <summary>
    /// Total turnover of all the outlets owned by same customer for the last X months
    /// </summary>
    public decimal SumCcxVal { get; set; }

    /// <summary>
    /// Total turnover ever
    /// </summary>
    public decimal? ValHistoric { get; set; }

    /// <summary>
    /// Most recent purchase order date
    /// </summary>
    public DateOnly? LastFch { get; set; }

    /// <summary>
    /// True if the sale point is enabled to participate as a raffles prize pickup point
    /// </summary>
    public bool Raffles { get; set; }

    /// <summary>
    /// True if Premium Line enabled
    /// </summary>
    public Guid? PremiumLine { get; set; }

    public virtual Tpa BrandNavigation { get; set; } = null!;

    public virtual Stp CategoryNavigation { get; set; } = null!;

    public virtual CliGral? CcxNavigation { get; set; }

    public virtual CliGral ClientNavigation { get; set; } = null!;

    public virtual Country? CountryNavigation { get; set; }

    public virtual Dept? DeptNavigation { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual PremiumLine? PremiumLineNavigation { get; set; }

    public virtual CliGral? ProveidorNavigation { get; set; }

    public virtual Art? SkuNavigation { get; set; }
}
