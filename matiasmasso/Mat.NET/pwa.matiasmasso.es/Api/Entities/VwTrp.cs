using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Transport companies
    /// </summary>
    public partial class VwTrp
    {
        public Guid TrpGuid { get; set; }
        public string TrpNom { get; set; } = null!;
        public string TrpNif { get; set; } = null!;
        public string? TrackingUrlTemplate { get; set; }
    }
}
