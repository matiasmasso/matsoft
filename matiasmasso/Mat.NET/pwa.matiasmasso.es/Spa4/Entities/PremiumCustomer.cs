using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customers included/excluded from Premium Line range distribution
    /// </summary>
    public partial class PremiumCustomer
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent PremiumLine table
        /// </summary>
        public Guid PremiumLine { get; set; }
        /// <summary>
        /// Foreign key to CliGral table
        /// </summary>
        public Guid Customer { get; set; }
        /// <summary>
        /// Enumerable DTOPremiumCustomer.Codis (1.Included, 2.Excluded)
        /// </summary>
        public int Codi { get; set; }
        /// <summary>
        /// Comments explaining why this customer is included or excluded
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Pdf contract; foreign key to Docfile table
        /// </summary>
        public string? Docfile { get; set; }
        /// <summary>
        /// User who created this entry
        /// </summary>
        public Guid UsrCreated { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Last user who edited this entry
        /// </summary>
        public Guid UsrLastEdited { get; set; }
        /// <summary>
        /// Date this entry was edited for last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }

        public virtual CliGral CustomerNavigation { get; set; } = null!;
        public virtual DocFile? DocfileNavigation { get; set; }
        public virtual PremiumLine PremiumLineNavigation { get; set; } = null!;
        public virtual Email UsrCreatedNavigation { get; set; } = null!;
        public virtual Email UsrLastEditedNavigation { get; set; } = null!;
    }
}
