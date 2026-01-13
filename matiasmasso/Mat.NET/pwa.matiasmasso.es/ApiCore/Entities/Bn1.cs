using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Bank entities
/// </summary>
public partial class Bn1
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Country of residence of the bank
    /// </summary>
    public Guid Country { get; set; }

    /// <summary>
    /// Bank national code
    /// </summary>
    public string? Bn11 { get; set; }

    /// <summary>
    /// Bank friendly name
    /// </summary>
    public string? Abr { get; set; }

    /// <summary>
    /// Bank corporate name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Default bank phone number
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// Bank logo 48x48 pixels
    /// </summary>
    public byte[]? Logo48 { get; set; }

    /// <summary>
    /// True if the bank is no longer active
    /// </summary>
    public bool Obsoleto { get; set; }

    /// <summary>
    /// Swift (BIC) code
    /// </summary>
    public string? Swift { get; set; }

    /// <summary>
    /// Website
    /// </summary>
    public string? Web { get; set; }

    /// <summary>
    /// True if the bank is member of Single Euro Payments Area
    /// </summary>
    public bool Sepa { get; set; }

    public string? Svg { get; set; }

    public virtual ICollection<Bn2> Bn2s { get; set; } = new List<Bn2>();
}
