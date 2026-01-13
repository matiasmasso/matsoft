using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Returns a name for each user, either if name, nickname or email address
    /// </summary>
    public partial class VwUsrNickname
    {
        public Guid Guid { get; set; }
        public string? Nom { get; set; }
    }
}
