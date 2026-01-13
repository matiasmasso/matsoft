using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwFilter
{
    public Guid FilterGuid { get; set; }

    public int FilterOrd { get; set; }

    public string? FilterEsp { get; set; }

    public string? FilterCat { get; set; }

    public string? FilterEng { get; set; }

    public string? FilterPor { get; set; }

    public Guid? FilterItemGuid { get; set; }

    public int? FilterItemOrd { get; set; }

    public string? FilterItemEsp { get; set; }

    public string? FilterItemCat { get; set; }

    public string? FilterItemEng { get; set; }

    public string? FilterItemPor { get; set; }
}
