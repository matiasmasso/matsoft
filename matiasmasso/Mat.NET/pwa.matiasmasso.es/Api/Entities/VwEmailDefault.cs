using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwEmailDefault
    {
        public Guid ContactGuid { get; set; }
        public Guid EmailGuid { get; set; }
        public string Adr { get; set; } = null!;
    }
}
