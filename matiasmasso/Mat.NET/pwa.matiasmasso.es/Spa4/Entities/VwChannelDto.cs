using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Default product discounts on each channel to calculate distributors price list
    /// </summary>
    public partial class VwChannelDto
    {
        public Guid Guid { get; set; }
        public Guid Channel { get; set; }
        public Guid? Product { get; set; }
        public decimal Dto { get; set; }
        public DateTime Fch { get; set; }
        public string? Obs { get; set; }
    }
}
