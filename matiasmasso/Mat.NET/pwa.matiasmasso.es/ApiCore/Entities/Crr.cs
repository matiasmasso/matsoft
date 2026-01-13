using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Correspondence documents
/// </summary>
public partial class Crr
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
    /// Year of issue/reception
    /// </summary>
    public short Yea { get; set; }

    /// <summary>
    /// Sequential number, unique for each Company/year
    /// </summary>
    public int Crr1 { get; set; }

    /// <summary>
    /// Date of issue/reception
    /// </summary>
    public DateOnly Fch { get; set; }

    /// <summary>
    /// Enumerable DTOCorrespondencia.Cods (1.received, 2.sent)
    /// </summary>
    public byte Rt { get; set; }

    /// <summary>
    /// Subject
    /// </summary>
    public string Dsc { get; set; } = null!;

    /// <summary>
    /// Destination person
    /// </summary>
    public string Atn { get; set; } = null!;

    /// <summary>
    /// Document; foreign key for Docfile table
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// User who created this entry; foreign key for Email table
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// User who edited this entry for kast time; foreign key for Email table
    /// </summary>
    public Guid? UsrLastEdited { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Date this entry was edited for last time
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual DocFile? HashNavigation { get; set; }

    public virtual Email? UsrCreatedNavigation { get; set; }

    public virtual Email? UsrLastEditedNavigation { get; set; }

    public virtual ICollection<CliGral> Clis { get; set; } = new List<CliGral>();
}
