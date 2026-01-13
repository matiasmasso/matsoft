using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class DocRel
{
    public Guid Guid { get; set; }

    public string Nom { get; set; } = null!;

    public int Sex { get; set; }

    public bool SubjectePassiu { get; set; }

    public string Ord { get; set; } = null!;

    public int Cod { get; set; }

    public virtual ICollection<DocTarget> DocTargets { get; set; } = new List<DocTarget>();
}
