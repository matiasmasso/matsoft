using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Bank Sepa mandates signed by us to our creditors
    /// </summary>
    public partial class SepaMe
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Bank entity; foreign key for CliBnc table
        /// </summary>
        public Guid Banc { get; set; }
        /// <summary>
        /// Creditor issuing the Sepa mandate; foreign key for CliGral table
        /// </summary>
        public Guid Lliurador { get; set; }
        /// <summary>
        /// Date from which this mandate is valid
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Mandate unique reference the creditor should mention on each bank charge under this mandate
        /// </summary>
        public string? Ref { get; set; }
        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime? FchTo { get; set; }
        /// <summary>
        /// Mandate pdf document; foreign key for Docfile table
        /// </summary>
        public string? Hash { get; set; }
        /// <summary>
        /// User who registered this document; foreign key for Email table
        /// </summary>
        public Guid? UsrCreated { get; set; }
        /// <summary>
        /// Date this record was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who edited this record for kast time
        /// </summary>
        public Guid? UsrLastEdited { get; set; }
        /// <summary>
        /// Date this record was edited for last time
        /// </summary>
        public DateTime? FchLastEdited { get; set; }

        public virtual CliBnc BancNavigation { get; set; } = null!;
        public virtual DocFile? HashNavigation { get; set; }
        public virtual CliGral LliuradorNavigation { get; set; } = null!;
        public virtual Email? UsrCreatedNavigation { get; set; }
        public virtual Email? UsrLastEditedNavigation { get; set; }
    }
}
