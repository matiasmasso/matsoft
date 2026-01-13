using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Users per contact
    /// </summary>
    public partial class EmailCli
    {
        /// <summary>
        /// User. Foreign key for Email table
        /// </summary>
        public Guid EmailGuid { get; set; }
        /// <summary>
        /// Contact. Foreign key for CliGral table
        /// </summary>
        public Guid ContactGuid { get; set; }
        /// <summary>
        /// Sort order in which this user should appear within this contact
        /// </summary>
        public int Ord { get; set; }

        public virtual CliGral ContactGu { get; set; } = null!;
        public virtual Email EmailGu { get; set; } = null!;
    }
}
