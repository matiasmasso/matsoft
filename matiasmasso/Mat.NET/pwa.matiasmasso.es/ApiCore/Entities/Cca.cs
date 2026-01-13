using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Account entries
/// </summary>
public partial class Cca
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
    /// Date year
    /// </summary>
    public short Yea { get; set; }

    /// <summary>
    /// Account entry number; unique for each combination of Company and year
    /// </summary>
    public int Cca1 { get; set; }

    /// <summary>
    /// Concept, free text
    /// </summary>
    public string? Txt { get; set; }

    /// <summary>
    /// Entry date
    /// </summary>
    public DateOnly? Fch { get; set; }

    /// <summary>
    /// Sort number (for example invoice number on invoices which help to sort invoice entries of same day in the right order)
    /// </summary>
    public int? Cdn { get; set; }

    /// <summary>
    /// Enumerable DTOCca.CcdEnum for entry purpose, used on sorting together with Cdn field
    /// </summary>
    public int? Ccd { get; set; }

    /// <summary>
    /// Project when applicable; foreign key to Projecte
    /// </summary>
    public Guid? Projecte { get; set; }

    /// <summary>
    /// Reference when applicable; foreign key to different tables depending on type of entry
    /// </summary>
    public Guid? Ref { get; set; }

    /// <summary>
    /// Hash of support document when applicable, foreign key for Docfile table
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// Date and time the entry was registered
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Date and time the entry was last updated
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    /// <summary>
    /// User who registered the entry; foreign key for Email table
    /// </summary>
    public Guid? UsrCreatedGuid { get; set; }

    /// <summary>
    /// User who updated this entry for last time, foreign key for Email table
    /// </summary>
    public Guid? UsrLastEditedGuid { get; set; }

    public virtual BancTransferPool? BancTransferPool { get; set; }

    public virtual BookFra? BookFra { get; set; }

    public virtual ICollection<Ccb> Ccbs { get; set; } = new List<Ccb>();

    public virtual ICollection<Csa> Csas { get; set; } = new List<Csa>();

    public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaders { get; set; } = new List<EdiRemadvHeader>();

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<Fra> Fras { get; set; } = new List<Fra>();

    public virtual DocFile? HashNavigation { get; set; }

    public virtual ICollection<Impagat> Impagats { get; set; } = new List<Impagat>();

    public virtual ICollection<Mr2> Mr2s { get; set; } = new List<Mr2>();

    public virtual ICollection<Mrt> MrtAltaCcaNavigations { get; set; } = new List<Mrt>();

    public virtual ICollection<Mrt> MrtBaixaCcaNavigations { get; set; } = new List<Mrt>();

    public virtual Nomina? Nomina { get; set; }

    public virtual Projecte? ProjecteNavigation { get; set; }

    public virtual Email? UsrCreated { get; set; }

    public virtual Email? UsrLastEdited { get; set; }

    public virtual ICollection<Xec> XecCcaPresentacioNavigations { get; set; } = new List<Xec>();

    public virtual ICollection<Xec> XecCcaRebutNavigations { get; set; } = new List<Xec>();

    public virtual ICollection<Xec> XecCcaVtoNavigations { get; set; } = new List<Xec>();
}
