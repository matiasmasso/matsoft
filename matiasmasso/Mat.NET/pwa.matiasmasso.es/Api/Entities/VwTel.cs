using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact phone numbers
    /// </summary>
    public partial class VwTel
    {
        public Guid Contact { get; set; }
        public string TelNum { get; set; } = null!;
    }
}
