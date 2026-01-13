using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Ecidept
{
    public Guid Guid { get; set; }

    public string Id { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Tel { get; set; }

    public Guid? Manager { get; set; }

    public Guid? Assistant { get; set; }

    public string? PlantillaModSkuWeekDays { get; set; }

    public string? PlantillaModSkuDocfile { get; set; }

    public DateTime? FchCreated { get; set; }
}
