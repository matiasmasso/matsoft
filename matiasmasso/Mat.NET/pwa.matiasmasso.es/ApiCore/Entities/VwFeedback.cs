using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwFeedback
{
    public Guid Target { get; set; }

    public Guid? UserOrCustomer { get; set; }

    public int? Likes { get; set; }

    public int? Shares { get; set; }

    public int? HasLiked { get; set; }

    public int? HasShared { get; set; }
}
