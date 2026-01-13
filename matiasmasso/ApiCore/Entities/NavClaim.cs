using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class NavClaim
{
    public Guid Nav { get; set; }

    public Guid Claim { get; set; }

    public virtual Claim ClaimNavigation { get; set; } = null!;

    public virtual Nav NavNavigation { get; set; } = null!;
}
