using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Pending debts and credits
/// </summary>
public partial class Pnd
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Debtor or creditor; foreign key for CliGral table
    /// </summary>
    public Guid ContactGuid { get; set; }

    /// <summary>
    /// Accounts entry; key of Cca table
    /// </summary>
    public Guid? CcaGuid { get; set; }

    /// <summary>
    /// Accounts account, key of PgcCta table
    /// </summary>
    public Guid? CtaGuid { get; set; }

    /// <summary>
    /// Invoice; key of Fra table
    /// </summary>
    public Guid? FraGuid { get; set; }

    /// <summary>
    /// Remittance; key of  table
    /// </summary>
    public Guid? CsbGuid { get; set; }

    /// <summary>
    /// Company; key of Emp table
    /// </summary>
    public short Emp { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    public DateOnly? Fch { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    public DateOnly? Vto { get; set; }

    /// <summary>
    /// Amount, in foreign currency
    /// </summary>
    public decimal Pts { get; set; }

    /// <summary>
    /// ISO 4217 Currency code
    /// </summary>
    public string Div { get; set; } = null!;

    /// <summary>
    /// Amount, in Euros
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Enumerable DTOPnd.Codis (1.debit, 2.credit)
    /// </summary>
    public string Ad { get; set; } = null!;

    /// <summary>
    /// Year of the invoice, if any
    /// </summary>
    public short Yef { get; set; }

    /// <summary>
    /// Invoice number, if any
    /// </summary>
    public string? Fra { get; set; }

    /// <summary>
    /// Payment way, free text
    /// </summary>
    public string? Fpg { get; set; }

    /// <summary>
    /// Payment way, enumerable DTOPnd.FormasDePagament
    /// </summary>
    public short Cfp { get; set; }

    /// <summary>
    /// Payment status, enumerable DTOPnd.StatusCod (0.Pending...)
    /// </summary>
    public short Status { get; set; }

    /// <summary>
    /// Foreign key for the table relevant to this debt
    /// </summary>
    public Guid? StatusGuid { get; set; }

    /// <summary>
    /// Date and time this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual CliGral Contact { get; set; } = null!;

    public virtual ICollection<XecDetail> XecDetails { get; set; } = new List<XecDetail>();
}
