using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Bank checks from debtors
/// </summary>
public partial class Xec
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Debtor; foreign key for CliGral table
    /// </summary>
    public Guid? ContactGuid { get; set; }

    /// <summary>
    /// Bank branch issuer, foreign key for Bn2 table
    /// </summary>
    public Guid? SbankBranch { get; set; }

    /// <summary>
    /// Check account number
    /// </summary>
    public string Iban { get; set; } = null!;

    /// <summary>
    /// Check number
    /// </summary>
    public string XecNum { get; set; } = null!;

    /// <summary>
    /// Amount, in foreign currency
    /// </summary>
    public decimal Pts { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Amount, in Euro
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Due date, in case of deferred amount
    /// </summary>
    public DateOnly? Vto { get; set; }

    /// <summary>
    /// Date the check was received
    /// </summary>
    public DateOnly? FchRecepcio { get; set; }

    /// <summary>
    /// Enumerable DTOXec.ModalitatsPresentacio (a la vista, al cobro, al descuento)
    /// </summary>
    public short CodPresentacio { get; set; }

    /// <summary>
    /// Enumerable DTOXec.StatusCods (En carftera, en circulació, vençut...)
    /// </summary>
    public short StatusCod { get; set; }

    /// <summary>
    /// The bank account where the xec was entered, if any; foreign key to CliBnc
    /// </summary>
    public Guid? NbancGuid { get; set; }

    /// <summary>
    /// The accounts entry reflecting check reception
    /// </summary>
    public Guid? CcaRebut { get; set; }

    /// <summary>
    /// The accounts entry reflecting the deposit of the x¡check in our bank
    /// </summary>
    public Guid? CcaPresentacio { get; set; }

    /// <summary>
    /// The accounts entry reflecting the date it was due in case of deferred amounts
    /// </summary>
    public Guid? CcaVto { get; set; }

    public virtual Cca? CcaPresentacioNavigation { get; set; }

    public virtual Cca? CcaRebutNavigation { get; set; }

    public virtual Cca? CcaVtoNavigation { get; set; }

    public virtual CliGral? Contact { get; set; }

    public virtual CliBnc? Nbanc { get; set; }

    public virtual Bn2? SbankBranchNavigation { get; set; }

    public virtual ICollection<XecDetail> XecDetails { get; set; } = new List<XecDetail>();
}
