using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contract purposes
    /// </summary>
    public partial class ContractCodi
    {
        public ContractCodi()
        {
            Contracts = new HashSet<Contract>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// True if refers to a depreciable asset
        /// </summary>
        public bool Amortitzable { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Id { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
