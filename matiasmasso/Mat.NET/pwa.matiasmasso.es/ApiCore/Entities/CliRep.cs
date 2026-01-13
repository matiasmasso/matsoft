using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Commercial agents (Reps) details
/// </summary>
public partial class CliRep
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Foreign key to rep company name if any. Foreign key to CliGral
    /// </summary>
    public Guid? CcxGuid { get; set; }

    /// <summary>
    /// Coloquial name to identify him internally
    /// </summary>
    public string? Abr { get; set; }

    /// <summary>
    /// Enytry date
    /// </summary>
    public DateOnly? Desde { get; set; }

    /// <summary>
    /// Termination date
    /// </summary>
    public DateOnly? Hasta { get; set; }

    /// <summary>
    /// Standard commission percentage
    /// </summary>
    public decimal ComStd { get; set; }

    /// <summary>
    /// Reduced commission percentage, to apply when it is so agreed
    /// </summary>
    public decimal ComRed { get; set; }

    /// <summary>
    /// VAT enumerable DTORep.IvaCods: 0.Exempt, 1.Standard
    /// </summary>
    public byte Iva { get; set; }

    /// <summary>
    /// Enumerable DTOProveidor.IrpfCods: 0.Exempt, 1.Standard, 2.Reduced, 3.Custom
    /// </summary>
    public byte Irpf { get; set; }

    /// <summary>
    /// Custom Irpf percentage
    /// </summary>
    public decimal IrpfTipus { get; set; }

    /// <summary>
    /// Free text describing the areas covered
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// If true, no commission statements will be issued
    /// </summary>
    public bool DisableLiqs { get; set; }

    /// <summary>
    /// Agent picture
    /// </summary>
    public byte[]? Foto { get; set; }

    /// <summary>
    /// Outdated account, no longer active
    /// </summary>
    public byte Obsoleto { get; set; }

    public virtual CliGral? Ccx { get; set; }

    public virtual CliGral Gu { get; set; } = null!;

    public virtual ICollection<RepCliCom> RepCliComs { get; set; } = new List<RepCliCom>();

    public virtual ICollection<RepProduct> RepProducts { get; set; } = new List<RepProduct>();
}
