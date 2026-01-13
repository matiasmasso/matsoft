using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Raffle participants
    /// </summary>
    public partial class SorteoLead
    {
        public SorteoLead()
        {
            Sorteos = new HashSet<Sorteo>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Raffle. Foreign key for Sorteos table
        /// </summary>
        public Guid Sorteo { get; set; }
        /// <summary>
        /// User. Foreign key for Email table
        /// </summary>
        public Guid Lead { get; set; }
        /// <summary>
        /// Choosen answer. Index of available answers, starting with zero
        /// </summary>
        public int? Answer { get; set; }
        /// <summary>
        /// Date and time of participation
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Retailer the participant selected to receive his prize in case he wins the raffle. Foreign key for CliGral
        /// </summary>
        public Guid? Distributor { get; set; }
        /// <summary>
        /// Sequential number, unique to each Raffle. Each participant gets a unique number of participation
        /// </summary>
        public int? Num { get; set; }

        public virtual CliGral? DistributorNavigation { get; set; }
        public virtual Email LeadNavigation { get; set; } = null!;
        public virtual Sorteo SorteoNavigation { get; set; } = null!;
        public virtual ICollection<Sorteo> Sorteos { get; set; }
    }
}
