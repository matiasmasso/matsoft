using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class NavRol
    {
        public Guid Nav { get; set; }
        public int Rol { get; set; }

        public virtual Nav NavNavigation { get; set; } = null!;
    }
}
