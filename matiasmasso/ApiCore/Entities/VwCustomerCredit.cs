using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerCredit
{
    public Guid CliGuid { get; set; }

    public decimal? Eur { get; set; }

    public DateTime FchCreated { get; set; }

    public string? Obs { get; set; }
}
