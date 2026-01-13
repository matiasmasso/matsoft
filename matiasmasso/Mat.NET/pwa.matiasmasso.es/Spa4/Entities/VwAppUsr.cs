using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// For each combination of user, device and App, shows app version and interval it has been logged in
    /// </summary>
    public partial class VwAppUsr
    {
        public DateTime? FirstFch { get; set; }
        public DateTime? LastFch { get; set; }
        public Guid? DeviceId { get; set; }
        public string? DeviceModel { get; set; }
        public string? AppVersion { get; set; }
        public string? Usr { get; set; }
    }
}
