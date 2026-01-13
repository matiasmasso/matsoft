using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Vehicle commercial brands
/// </summary>
public partial class VehicleMarca
{
    /// <summary>
    /// Primary kley
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// &apos;&apos;
    /// </summary>
    public string Nom { get; set; } = null!;

    public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
}
