using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CustomerDept
{
    public Guid Guid { get; set; }

    public Guid Customer { get; set; }

    public string Cod { get; set; } = null!;

    public string? Nom { get; set; }

    public virtual ICollection<CustomerDeptItem> CustomerDeptItems { get; set; } = new List<CustomerDeptItem>();

    public virtual CliGral CustomerNavigation { get; set; } = null!;
}
