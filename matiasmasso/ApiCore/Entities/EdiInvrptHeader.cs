using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class EdiInvrptHeader
{
    public Guid Guid { get; set; }

    /// <summary>
    /// Message sender party
    /// </summary>
    public string Nadms { get; set; } = null!;

    /// <summary>
    /// Stock holder party
    /// </summary>
    public string Nadgy { get; set; } = null!;

    public Guid? Customer { get; set; }

    public string? DocNum { get; set; }

    public DateTime Fch { get; set; }

    public DateTime FchCreated { get; set; }

    public virtual ICollection<EdiInvrptItem> EdiInvrptItems { get; set; } = new List<EdiInvrptItem>();
}
