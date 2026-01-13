using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwBrand
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public int Ord { get; set; }

    public Guid? Proveidor { get; set; }

    public Guid? CnapGuid { get; set; }

    public Guid? MadeIn { get; set; }

    public string? CodiMercancia { get; set; }

    public int CodDist { get; set; }

    public int Color { get; set; }

    public int? WebAtlasDeadline { get; set; }

    public int? WebAtlasRafflesDeadline { get; set; }

    public bool WebEnabledConsumer { get; set; }

    public bool WebEnabledPro { get; set; }

    public bool ShowAtlas { get; set; }

    public bool IncludeDeptOnUrl { get; set; }

    public bool EnLiquidacio { get; set; }

    public bool Obsoleto { get; set; }

    public byte[]? Logo { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime FchLastEdited { get; set; }

    public int Src { get; set; }

    public string? Esp { get; set; }

    public string? Cat { get; set; }

    public string? Eng { get; set; }

    public string? Por { get; set; }
}
