using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwContentUrl
    {
        public Guid Target { get; set; }
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }
    }
}
