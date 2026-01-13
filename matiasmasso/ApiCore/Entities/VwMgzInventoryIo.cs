using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwMgzInventoryIo
{
    public Guid? MgzGuid { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public Guid Brand { get; set; }

    public Guid Category { get; set; }

    public Guid ArtGuid { get; set; }

    public int? Io { get; set; }
}
