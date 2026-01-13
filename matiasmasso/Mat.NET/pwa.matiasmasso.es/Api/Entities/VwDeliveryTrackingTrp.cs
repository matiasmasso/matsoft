using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Shipment forwarder details
    /// </summary>
    public partial class VwDeliveryTrackingTrp
    {
        public Guid? AlbGuid { get; set; }
        public int? Emp { get; set; }
        public int? Yea { get; set; }
        public int? Alb { get; set; }
        public Guid? TrpGuid { get; set; }
        public string? TrpNom { get; set; }
        public string? TrpNif { get; set; }
        public string? Tracking { get; set; }
        public string? Expr1 { get; set; }
    }
}
