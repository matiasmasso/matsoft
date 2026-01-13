using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Irpf tax withholdings certificates
/// </summary>
public partial class CertificatIrpf
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Certificate owner, foreign key to CliGral table
    /// </summary>
    public Guid Contact { get; set; }

    /// <summary>
    /// Year the certificate was issued
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Period (0: anual)
    /// </summary>
    public int Period { get; set; }

    /// <summary>
    /// Foreign key to Docfile table
    /// </summary>
    public string? Hash { get; set; }

    public virtual CliGral ContactNavigation { get; set; } = null!;

    public virtual DocFile? HashNavigation { get; set; }
}
