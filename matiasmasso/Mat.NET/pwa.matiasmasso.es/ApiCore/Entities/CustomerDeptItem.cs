using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CustomerDeptItem
{
    public Guid Parent { get; set; }

    public Guid Product { get; set; }

    public virtual CustomerDept ParentNavigation { get; set; } = null!;
}
