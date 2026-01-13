using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Bank remittance items (efectos de las remesas bancarias)
/// </summary>
public partial class Csb
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to parent table Csa
    /// </summary>
    public Guid CsaGuid { get; set; }

    /// <summary>
    /// Consecutive payment number, unique on each remittance
    /// </summary>
    public short Doc { get; set; }

    /// <summary>
    /// Debtor name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Debt description
    /// </summary>
    public string? Txt { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    public DateTime? Vto { get; set; }

    /// <summary>
    /// Debtor account number
    /// </summary>
    public string? Ccc { get; set; }

    /// <summary>
    /// Customer; foreign key for CliGral table
    /// </summary>
    public Guid CliGuid { get; set; }

    /// <summary>
    /// Year of invoice, if any
    /// </summary>
    public short? Yef { get; set; }

    /// <summary>
    /// Invoice, if any
    /// </summary>
    public int? Fra { get; set; }

    /// <summary>
    /// Debt amount, in foreign currency
    /// </summary>
    public decimal Val { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Cur { get; set; } = null!;

    /// <summary>
    /// Debt amount, in Euro
    /// </summary>
    public decimal Eur { get; set; }

    /// <summary>
    /// Bank mandate; foreign key for Iban table
    /// </summary>
    public Guid? SepaMandato { get; set; }

    /// <summary>
    /// Tipo de Adeudo AEB_SEPA_B2B Enumerable DTOCsb.TiposAdeudo (NotSet,FRST,RCUR,FNAL,OOF)
    /// </summary>
    public short SepaTipoAdeudo { get; set; }

    /// <summary>
    /// Enumerable DTOCsb.Results (Pending, due, unpaid, claimed)
    /// </summary>
    public int Result { get; set; }

    /// <summary>
    /// In case the bank advances payments, the account entry when the customer pays on due date
    /// </summary>
    public Guid? CcaVtoGuid { get; set; }

    public virtual CliGral Cli { get; set; } = null!;

    public virtual Csa Csa { get; set; } = null!;

    public virtual ICollection<Impagat> Impagats { get; set; } = new List<Impagat>();

    public virtual Iban? SepaMandatoNavigation { get; set; }
}
