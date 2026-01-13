using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Logs each time Wtbol external service (Britax through Hash) connects to our website to download affiliated customer stocks
    /// </summary>
    public partial class WtbolLog
    {
        /// <summary>
        /// Affiliated customer site; foreign key for WtbolSite
        /// </summary>
        public Guid Site { get; set; }
        /// <summary>
        /// Date and time Hatch connects
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Ip through which Hatch connects
        /// </summary>
        public string Ip { get; set; } = null!;
    }
}
