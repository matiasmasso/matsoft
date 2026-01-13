using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customers holdings, used for statistics gathering different customers from a unique commercial group
    /// </summary>
    public partial class Holding
    {
        public Holding()
        {
            CliClients = new HashSet<CliClient>();
            CustomerClusters = new HashSet<CustomerCluster>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string? Nom { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual ICollection<CliClient> CliClients { get; set; }
        public virtual ICollection<CustomerCluster> CustomerClusters { get; set; }
    }
}
