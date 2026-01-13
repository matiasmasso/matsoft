using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Log of users who have browsed the documents
    /// </summary>
    public partial class DocFileLog
    {
        /// <summary>
        /// Document; foreign key for Docfile table
        /// </summary>
        public string Hash { get; set; } = null!;
        /// <summary>
        /// Date and time the user browsed the document
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// User; foreign key for Email table
        /// </summary>
        public Guid? User { get; set; }

        public virtual DocFile HashNavigation { get; set; } = null!;
        public virtual Email? UserNavigation { get; set; }
    }
}
