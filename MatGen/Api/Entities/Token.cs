using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Token
    {
        public Guid User { get; set; }
        public int Provider { get; set; }
        public string? Value { get; set; }
        public string? Email { get; set; }
        public DateTime? Expires { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        public virtual User UserNavigation { get; set; } = null!;
    }
}
