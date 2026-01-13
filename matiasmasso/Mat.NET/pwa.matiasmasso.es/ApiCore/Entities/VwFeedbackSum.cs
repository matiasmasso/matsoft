using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwFeedbackSum
{
    public Guid Target { get; set; }

    public int? Shares { get; set; }
}
