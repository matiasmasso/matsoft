using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Contacts bank accounts
/// </summary>
public partial class Iban
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Owner of the bank account; foreign key for CliGral table
    /// </summary>
    public Guid ContactGuid { get; set; }

    /// <summary>
    /// Owner rol. Enumerable DTOIban.Cods (supplier, customer, staff, bank...)
    /// </summary>
    public short Cod { get; set; }

    /// <summary>
    /// Account number (24 digits in case of Spanish Iban)
    /// </summary>
    public string Ccc { get; set; } = null!;

    /// <summary>
    /// Issue date of mandate
    /// </summary>
    public DateTime? MandatoFch { get; set; }

    /// <summary>
    /// Date the mandate is no longer valid
    /// </summary>
    public DateTime? CaducaFch { get; set; }

    /// <summary>
    /// Bank branch. Foreign key for Bn2 table
    /// </summary>
    public Guid? BankBranch { get; set; }

    /// <summary>
    /// Enumerable DTOIban.Formats (SEPA B2B, SEPA Core...)
    /// </summary>
    public int Format { get; set; }

    /// <summary>
    /// Enumerable DTOIban.StatusEnum (uploaded, downloaded, pending, aproved...)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Mandate Pdf document. Foreign key to Docfile table
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// Date the bank account owner downloaded the empty mandate form to be filled
    /// </summary>
    public DateTime? FchDownloaded { get; set; }

    /// <summary>
    /// User who downloaded the empty mandate; foreign key to Email table
    /// </summary>
    public Guid? UsrDownloaded { get; set; }

    /// <summary>
    /// Date the bank account owner uploaded the completed mandate form
    /// </summary>
    public DateTime? FchUploaded { get; set; }

    /// <summary>
    /// User who uploaded the completed mandate form; foreign key for Email table
    /// </summary>
    public Guid? UsrUploaded { get; set; }

    /// <summary>
    /// Date the mandate was aproved by our company staff, verifying it was correctly filled and signed
    /// </summary>
    public DateTime? FchApproved { get; set; }

    /// <summary>
    /// Corporate user who approved the mandate; foreign key to Email table
    /// </summary>
    public Guid? UsrApproved { get; set; }

    /// <summary>
    /// The name of the person from the bank account owner organisation who signed the mandate
    /// </summary>
    public string? PersonNom { get; set; }

    /// <summary>
    /// VAT number of the person who signed the mandate
    /// </summary>
    public string? PersonDni { get; set; }

    /// <summary>
    /// Date this record was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual Bn2? BankBranchNavigation { get; set; }

    public virtual CliGral Contact { get; set; } = null!;

    public virtual ICollection<Csb> Csbs { get; set; } = new List<Csb>();

    public virtual Email? UsrApprovedNavigation { get; set; }

    public virtual Email? UsrDownloadedNavigation { get; set; }

    public virtual Email? UsrUploadedNavigation { get; set; }
}
