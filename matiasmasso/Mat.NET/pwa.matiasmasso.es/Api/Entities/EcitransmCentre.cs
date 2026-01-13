using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// El Corte Ingles centers and the shipment groups they belong to share a common delivery platform
    /// </summary>
    public partial class EcitransmCentre
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table ECITransmGroup
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Final destination center; foreign key for CliGral table
        /// </summary>
        public Guid Centre { get; set; }

        public virtual CliGral CentreNavigation { get; set; } = null!;
        public virtual EcitransmGroup ParentNavigation { get; set; } = null!;
    }
}
