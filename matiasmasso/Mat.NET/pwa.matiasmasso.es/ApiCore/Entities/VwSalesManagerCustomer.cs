using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwSalesManagerCustomer
{
    public Guid SalesManager { get; set; }

    public Guid Customer { get; set; }
}
