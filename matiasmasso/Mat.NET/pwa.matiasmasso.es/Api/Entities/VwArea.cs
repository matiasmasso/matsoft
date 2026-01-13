using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Returns area name from its Id regardless it&apos;s a Country, Zone or Location
    /// </summary>
    public partial class VwArea
    {
        /// <summary>
        /// Area code: 1:Country, 2:Zona, 3:Location
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Primary key, either from a Country, Zona or Location table
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Name of the area
        /// </summary>
        public string Nom { get; set; } = null!;
    }
}
