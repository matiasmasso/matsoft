using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwAppUsrLog
    {
        public string? Adr { get; set; }
        public string? Nickname { get; set; }
        public DateTime Fch { get; set; }
        public DateTime? FchTo { get; set; }
        public string? AppVersion { get; set; }
        public string? Os { get; set; }
        public string? DeviceModel { get; set; }
        public string? DeviceId { get; set; }
    }
}
