using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwProductLangText
{
    public Guid Guid { get; set; }

    public int Cod { get; set; }

    public int? Src { get; set; }

    public string? Lang { get; set; }

    public string? BrandText { get; set; }

    public string? DeptText { get; set; }

    public string? CategoryText { get; set; }

    public string? SkuText { get; set; }
}
