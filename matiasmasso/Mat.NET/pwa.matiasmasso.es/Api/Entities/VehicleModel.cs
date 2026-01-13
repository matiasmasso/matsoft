using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Vehicle brand commercial models
    /// </summary>
    public partial class VehicleModel
    {
        public VehicleModel()
        {
            VehicleFlota = new HashSet<VehicleFlotum>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Vehicle brand, foreign key to Vehicle_Marcas
        /// </summary>
        public Guid? MarcaGuid { get; set; }
        /// <summary>
        /// Brand model name
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual VehicleMarca? MarcaGu { get; set; }
        public virtual ICollection<VehicleFlotum> VehicleFlota { get; set; }
    }
}
