using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Vw4momsSkuStock
{
    public Guid Brand { get; set; }

    public Guid? SkuGuid { get; set; }

    public int? Stock { get; set; }

    public int? Clients { get; set; }

    public int? Pot { get; set; }

    public int? ClientsBlockStock { get; set; }

    public int? ClientsEnProgramacio { get; set; }
}
