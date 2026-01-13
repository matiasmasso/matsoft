using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contact search keys
    /// </summary>
    public partial class Cll
    {
        /// <summary>
        /// Search key
        /// </summary>
        public string Cll1 { get; set; } = null!;
        /// <summary>
        /// Foreign key for CliGral table
        /// </summary>
        public Guid ContactGuid { get; set; }
        /// <summary>
        /// Enumerable DTOContact.ContactKeys (0.Name, 3.Location, 4.Commercial name, 26.Custom search key)
        /// </summary>
        public short Cod { get; set; }

        public virtual CliGral ContactGu { get; set; } = null!;
    }
}
