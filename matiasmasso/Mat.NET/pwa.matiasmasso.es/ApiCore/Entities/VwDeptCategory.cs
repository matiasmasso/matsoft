using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwDeptCategory
{
    public Guid Dept { get; set; }

    public Guid Category { get; set; }
}
