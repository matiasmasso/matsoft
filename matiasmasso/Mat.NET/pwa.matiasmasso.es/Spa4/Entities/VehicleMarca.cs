using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Vehicle commercial brands
    /// </summary>
    public partial class VehicleMarca
    {
        public VehicleMarca()
        {
            VehicleModels = new HashSet<VehicleModel>();
        }

        /// <summary>
        /// Primary kley
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// &apos;&apos;
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
