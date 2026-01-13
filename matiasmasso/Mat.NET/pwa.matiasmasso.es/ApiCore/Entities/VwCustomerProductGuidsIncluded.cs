using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCustomerProductGuidsIncluded
{
    public Guid EmailGuid { get; set; }

    public Guid Product { get; set; }
}
