using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwDeliveryShipment
{
    public Guid AlbGuid { get; set; }

    public string? Expedition { get; set; }

    public DateOnly? Fch { get; set; }

    public int? Packages { get; set; }

    public string? Pallet { get; set; }

    public int? Package { get; set; }

    public string? Sscc { get; set; }

    public Guid ArcGuid { get; set; }

    public int? Qty { get; set; }

    public Guid SkuGuid { get; set; }

    public string? SkuEan { get; set; }

    public int Lin { get; set; }

    public string? TrpNif { get; set; }

    public Guid? TrpGuid { get; set; }

    public string? TrpNom { get; set; }

    public string? Tracking { get; set; }

    public decimal? CubicKg { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Volume { get; set; }

    public decimal? Cost { get; set; }

    public string? Packaging { get; set; }

    public int? Length { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string? TrackingUrlTemplate { get; set; }
}
