using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwBank
{
    public Guid BankGuid { get; set; }

    public string BankNom { get; set; } = null!;

    public string BankAlias { get; set; } = null!;

    public string BankSwift { get; set; } = null!;

    public Guid BankBranchGuid { get; set; }

    public string BankBranchAdr { get; set; } = null!;

    public Guid? BankBranchLocationGuid { get; set; }

    public string? BankBranchLocationNom { get; set; }

    public Guid? BankBranchZonaGuid { get; set; }

    public string? BankBranchZonaNom { get; set; }

    public Guid? BankBranchCountryGuid { get; set; }

    public string? BankBranchCountryIso { get; set; }
}
