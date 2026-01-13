using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCcxOrMe
{
    public Guid Guid { get; set; }

    public Guid? Ccx { get; set; }
}
