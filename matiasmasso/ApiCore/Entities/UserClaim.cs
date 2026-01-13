using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class UserClaim
{
    public Guid Usr { get; set; }

    public Guid Claim { get; set; }
}
