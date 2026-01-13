using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Shipments to El Corte Ingles customer are grouped by destinations which share a common delivery platform. This table stores the group name and the delivery platform, and its children on EciTransmCentre table store the final destination centers for the shipments
    /// </summary>
    public partial class EcitransmGroup
    {
        public EcitransmGroup()
        {
            EcitransmCentres = new HashSet<EcitransmCentre>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Group friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Delivery platform; foreign key for CliGral table
        /// </summary>
        public Guid? Platform { get; set; }

        public virtual CliGral? PlatformNavigation { get; set; }
        public virtual ICollection<EcitransmCentre> EcitransmCentres { get; set; }
    }
}
