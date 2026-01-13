using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwContactChannel
{
    public Guid Contact { get; set; }

    public Guid? Channel { get; set; }
}
