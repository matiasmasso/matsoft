using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwMediaResource
{
    public Guid Guid { get; set; }

    public string Hash { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public short Mime { get; set; }

    public short Cod { get; set; }

    public string? Lang { get; set; }

    public Guid? Product { get; set; }

    public byte[]? Thumbnail { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int Size { get; set; }

    public int Hres { get; set; }

    public int Vres { get; set; }

    public int Pags { get; set; }

    public int Ord { get; set; }

    public bool Obsoleto { get; set; }

    public Guid UsrCreated { get; set; }

    public Guid? UsrLastEdited { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime? FchLastEdited { get; set; }

    public string? DscEsp { get; set; }

    public string? DscCat { get; set; }

    public string? DscEng { get; set; }

    public string? DscPor { get; set; }

    public string LangSet { get; set; } = null!;

    public Guid? Expr1 { get; set; }

    public int? Src { get; set; }

    public string? Esp { get; set; }

    public string? Cat { get; set; }

    public string? Eng { get; set; }

    public string? Por { get; set; }

    public DateTime? FchEsp { get; set; }

    public DateTime? FchCat { get; set; }

    public DateTime? FchEng { get; set; }

    public DateTime? FchPor { get; set; }
}
