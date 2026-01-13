using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contact activity classification
    /// </summary>
    public partial class ContactClass
    {
        public ContactClass()
        {
            CliAperturas = new HashSet<CliApertura>();
            CliGrals = new HashSet<CliGral>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Distribution channel, if any. Foreign key for DistributionChannel table
        /// </summary>
        public Guid? DistributionChannel { get; set; }
        /// <summary>
        /// Name, Spanish
        /// </summary>
        public string Esp { get; set; } = null!;
        /// <summary>
        /// Name, Catalan
        /// </summary>
        public string? Cat { get; set; }
        /// <summary>
        /// Name, English
        /// </summary>
        public string? Eng { get; set; }
        /// <summary>
        /// Name, Portuguese
        /// </summary>
        public string? Por { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// True if members should be listed as sale points
        /// </summary>
        public bool SalePoint { get; set; }
        /// <summary>
        /// True if members should participate as raffle prize pick up point
        /// </summary>
        public bool Raffles { get; set; }

        public virtual DistributionChannel? DistributionChannelNavigation { get; set; }
        public virtual ICollection<CliApertura> CliAperturas { get; set; }
        public virtual ICollection<CliGral> CliGrals { get; set; }
    }
}
