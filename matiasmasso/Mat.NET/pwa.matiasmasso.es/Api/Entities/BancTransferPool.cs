using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Sets of bank transfers
    /// </summary>
    public partial class BancTransferPool
    {
        public BancTransferPool()
        {
            BancTransferBeneficiaris = new HashSet<BancTransferBeneficiari>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Accounts entry, foreign key for Cca table
        /// </summary>
        public Guid Cca { get; set; }
        /// <summary>
        /// Issuing bank, foreign key for CliBnc table
        /// </summary>
        public Guid BancEmissor { get; set; }
        /// <summary>
        /// Transfer reference returned by the bank
        /// </summary>
        public string? Ref { get; set; }

        public virtual CliBnc BancEmissorNavigation { get; set; } = null!;
        public virtual Cca CcaNavigation { get; set; } = null!;
        public virtual ICollection<BancTransferBeneficiari> BancTransferBeneficiaris { get; set; }
    }
}
