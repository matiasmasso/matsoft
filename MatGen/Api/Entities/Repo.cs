using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Repo
{
    public Guid Guid { get; set; }

    public string? Nom { get; set; }

    public string? Abr { get; set; }

    public string? Adr { get; set; }

    public string? Location { get; set; }

    public string? Zip { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<DocSrc> DocSrcs { get; set; } = new List<DocSrc>();
}
