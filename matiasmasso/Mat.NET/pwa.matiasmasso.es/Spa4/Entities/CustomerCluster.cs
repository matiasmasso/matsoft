using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Cluster definitions for a certain holding
    /// </summary>
    public partial class CustomerCluster
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Holding the cluster is defined for. Foreign key to Holding table
        /// </summary>
        public Guid Holding { get; set; }
        /// <summary>
        /// Cluster friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }

        public virtual Holding HoldingNavigation { get; set; } = null!;
    }
}
