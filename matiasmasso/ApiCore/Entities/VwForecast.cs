using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwForecast
{
    public Guid? Customer { get; set; }

    public Guid Sku { get; set; }

    public int Yea { get; set; }

    public int Mes { get; set; }

    public int Qty { get; set; }

    public DateTime FchCreated { get; set; }

    public Guid? UsrCreated { get; set; }
}
