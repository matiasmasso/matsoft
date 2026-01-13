using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Region details. A region belongs to a Country and is split on Provincias
    /// </summary>
    public partial class Regio
    {
        public Regio()
        {
            Provincia = new HashSet<Provincium>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table Country
        /// </summary>
        public Guid Country { get; set; }
        /// <summary>
        /// Region name
        /// </summary>
        public string? Nom { get; set; }

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual ICollection<Provincium> Provincia { get; set; }
    }
}
