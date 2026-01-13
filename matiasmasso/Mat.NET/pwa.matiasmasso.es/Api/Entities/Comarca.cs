using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// County details. A County is an optional area inside a Zona
    /// </summary>
    public partial class Comarca
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Name of the Comarca
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Foreign key to Zona table where the Comarca belongs
        /// </summary>
        public Guid? Zona { get; set; }

        public virtual Zona? ZonaNavigation { get; set; }
    }
}
