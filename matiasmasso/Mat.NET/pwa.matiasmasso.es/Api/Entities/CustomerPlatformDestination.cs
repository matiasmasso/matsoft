using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Destination centers covered by each delivery platform
    /// </summary>
    public partial class CustomerPlatformDestination
    {
        /// <summary>
        /// Delivery platform; foreign key for CustomerPlatform table
        /// </summary>
        public Guid Platform { get; set; }
        /// <summary>
        /// Sales center, store or warehouse final destination of the deliveries sent to the platform; foreign key for CliGral table
        /// </summary>
        public Guid Destination { get; set; }

        public virtual CustomerPlatform Platform1 { get; set; } = null!;
        public virtual CliGral PlatformNavigation { get; set; } = null!;
    }
}
