using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwMgzInventoryMovement
    {
        public Guid? MgzGuid { get; set; }
        public Guid Brand { get; set; }
        public Guid? Category { get; set; }
        public Guid ArtGuid { get; set; }
        public DateTime Fch { get; set; }
        public int? Io { get; set; }
    }
}
