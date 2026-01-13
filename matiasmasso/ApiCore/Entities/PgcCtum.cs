using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Accounts accounts
/// </summary>
public partial class PgcCtum
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Accounts plan; foreign key for PgcPlan table
    /// </summary>
    public Guid? Plan { get; set; }

    /// <summary>
    /// Account Id, 5 digits
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Account name, Spanish language
    /// </summary>
    public string Esp { get; set; } = null!;

    /// <summary>
    /// Account name, Catalan language
    /// </summary>
    public string? Cat { get; set; }

    /// <summary>
    /// Account name, English language
    /// </summary>
    public string? Eng { get; set; }

    /// <summary>
    /// Account description, free text
    /// </summary>
    public string? Dsc { get; set; }

    /// <summary>
    /// Accounts group it belongs to; foreign key for PgcClass table
    /// </summary>
    public Guid? PgcClass { get; set; }

    /// <summary>
    /// Equivalent account on next plan
    /// </summary>
    public Guid? NextCtaGuid { get; set; }

    /// <summary>
    /// Enumerable DTOPgcCta.Acts (1.receivable account, 2.payable account)
    /// </summary>
    public byte Act { get; set; }

    /// <summary>
    /// Enumerable DTOPgcPlan.Ctas, used to programmatically refer to an account regardles of its Id
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// True if it is the base to calculate VAT charges
    /// </summary>
    public bool IsBaseImponibleIva { get; set; }

    /// <summary>
    /// True if it is a VAT charge
    /// </summary>
    public bool IsQuotaIva { get; set; }

    /// <summary>
    /// True it it is a Irpf charge (tax withholdings)
    /// </summary>
    public bool IsQuotaIrpf { get; set; }

    public virtual ICollection<BookFra> BookFras { get; set; } = new List<BookFra>();

    public virtual ICollection<CcaSchedItem> CcaSchedItems { get; set; } = new List<CcaSchedItem>();

    public virtual ICollection<Ccb> Ccbs { get; set; } = new List<Ccb>();

    public virtual ICollection<CliPrv> CliPrvCtaCarrecNavigations { get; set; } = new List<CliPrv>();

    public virtual ICollection<CliPrv> CliPrvCtaCreditoraNavigations { get; set; } = new List<CliPrv>();

    public virtual ICollection<PgcCtum> InverseNextCta { get; set; } = new List<PgcCtum>();

    public virtual ICollection<Mrt> Mrts { get; set; } = new List<Mrt>();

    public virtual PgcCtum? NextCta { get; set; }

    public virtual PgcClass? PgcClassNavigation { get; set; }

    public virtual PgcPlan? PlanNavigation { get; set; }
}
