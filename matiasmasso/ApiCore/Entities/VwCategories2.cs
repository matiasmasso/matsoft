using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCategories2
{
    public Guid Guid { get; set; }

    public int ImgExists { get; set; }

    public int Emp { get; set; }

    public int Ord { get; set; }

    public int Codi { get; set; }

    public Guid Brand { get; set; }

    public int Color { get; set; }

    public int? LaunchYear { get; set; }

    public int? LaunchMonth { get; set; }

    public Guid? MadeIn { get; set; }

    public Guid? CnapGuid { get; set; }

    public string? CodiMercancia { get; set; }

    public bool DscPropagateToChildren { get; set; }

    public bool WebEnabledPro { get; set; }

    public bool WebEnabledConsumer { get; set; }

    public bool IsBundle { get; set; }

    public bool NoStk { get; set; }

    public bool NoDimensions { get; set; }

    public decimal KgNet { get; set; }

    public decimal Kg { get; set; }

    public decimal M3 { get; set; }

    public double? DimensionL { get; set; }

    public double? DimensionW { get; set; }

    public double? DimensionH { get; set; }

    public int InnerPack { get; set; }

    public int OuterPack { get; set; }

    public bool ForzarInnerPack { get; set; }

    public DateTime? HideUntil { get; set; }

    public bool Obsoleto { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime FchLastEdited { get; set; }

    public int Src { get; set; }

    public string? Esp { get; set; }

    public string? Cat { get; set; }

    public string? Eng { get; set; }

    public string? Por { get; set; }
}
