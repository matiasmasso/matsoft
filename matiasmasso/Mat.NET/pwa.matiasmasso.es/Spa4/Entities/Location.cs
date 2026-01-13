using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Zona locations (towns, cities...)
    /// </summary>
    public partial class Location
    {
        public Location()
        {
            Bn2s = new HashSet<Bn2>();
            Emails = new HashSet<Email>();
            Webs = new HashSet<Web>();
            Zips = new HashSet<Zip>();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to Zona table
        /// </summary>
        public Guid Zona { get; set; }
        /// <summary>
        /// Location name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Foreign key to Comarca table
        /// </summary>
        public Guid? Comarca { get; set; }

        public virtual Zona ZonaNavigation { get; set; } = null!;
        public virtual ICollection<Bn2> Bn2s { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Web> Webs { get; set; }
        public virtual ICollection<Zip> Zips { get; set; }
    }
}
