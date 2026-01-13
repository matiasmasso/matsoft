using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Shipment tracking details, used on VwDeliveryShipment
    /// </summary>
    public partial class VwDeliveryTracking
    {
        public Guid Guid { get; set; }
        public Guid? AlbGuid { get; set; }
        public int? Emp { get; set; }
        public Guid? Log { get; set; }
        public DateTime? Fch { get; set; }
        public string? Sender { get; set; }
        public string? Delivery { get; set; }
        public int? Packages { get; set; }
        public string? Forwarder { get; set; }
        public int? Pallets { get; set; }
        public string? Tracking { get; set; }
        public decimal? CubicKg { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Cost { get; set; }
        public int? Package { get; set; }
        public string? Sscc { get; set; }
        public string? Packaging { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public Guid? TrpGuid { get; set; }
        public string? TrpNom { get; set; }
        public string? TrpNif { get; set; }
        public string? TrackingUrlTemplate { get; set; }
    }
}
