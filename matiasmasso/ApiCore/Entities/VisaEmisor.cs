using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Credit card issuers
/// </summary>
public partial class VisaEmisor
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Logo
    /// </summary>
    public byte[]? Img { get; set; }

    public virtual ICollection<VisaCard> VisaCards { get; set; } = new List<VisaCard>();
}
