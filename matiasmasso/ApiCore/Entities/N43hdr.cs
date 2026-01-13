using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class N43hdr
{
    public Guid Guid { get; set; }

    public string Ccc { get; set; } = null!;

    public DateOnly Fch { get; set; }

    public DateTime FchCreated { get; set; }

    public Guid UsrCreated { get; set; }

    public virtual ICollection<N43cca> N43ccas { get; set; } = new List<N43cca>();
}
