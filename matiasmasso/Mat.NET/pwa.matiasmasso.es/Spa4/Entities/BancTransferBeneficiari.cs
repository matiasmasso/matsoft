using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Transfer beneficiaries and amounts
    /// </summary>
    public partial class BancTransferBeneficiari
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Transfer pool, foreign key for BancTransferPool table
        /// </summary>
        public Guid Pool { get; set; }
        /// <summary>
        /// Beneficiari Id, foreign key for CliGral table
        /// </summary>
        public Guid Contact { get; set; }
        /// <summary>
        /// Bank branch id, foreign key for Bn2 table
        /// </summary>
        public Guid BankBranch { get; set; }
        /// <summary>
        /// Full beneficiary account number
        /// </summary>
        public string Account { get; set; } = null!;
        /// <summary>
        /// Transfer concept, free text
        /// </summary>
        public string? Concepte { get; set; }
        /// <summary>
        /// Transfer amount in Euro
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Transfer amount in foreign currency
        /// </summary>
        public decimal Val { get; set; }

        public virtual Bn2 BankBranchNavigation { get; set; } = null!;
        public virtual CliGral ContactNavigation { get; set; } = null!;
        public virtual BancTransferPool PoolNavigation { get; set; } = null!;
    }
}
