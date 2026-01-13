using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Postal codes
    /// </summary>
    public partial class Zip
    {
        public Zip()
        {
            Albs = new HashSet<Alb>();
            CliAdrs = new HashSet<CliAdr>();
            Fras = new HashSet<Fra>();
            Immobles = new HashSet<Immoble>();
            SatRecalls = new HashSet<SatRecall>();
            Spvs = new HashSet<Spv>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to Location table
        /// </summary>
        public Guid Location { get; set; }
        /// <summary>
        /// Postal code
        /// </summary>
        public string ZipCod { get; set; } = null!;

        public virtual Location LocationNavigation { get; set; } = null!;
        public virtual ICollection<Alb> Albs { get; set; }
        public virtual ICollection<CliAdr> CliAdrs { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<Immoble> Immobles { get; set; }
        public virtual ICollection<SatRecall> SatRecalls { get; set; }
        public virtual ICollection<Spv> Spvs { get; set; }
    }
}
