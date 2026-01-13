using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact default phone number
    /// </summary>
    public partial class VwTelDefault
    {
        public Guid Contact { get; set; }
        public string? TelNum { get; set; }
    }
}
