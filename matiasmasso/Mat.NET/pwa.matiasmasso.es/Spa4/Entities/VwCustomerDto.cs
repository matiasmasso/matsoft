using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product discount over retail price to calculate customer cost price list
    /// </summary>
    public partial class VwCustomerDto
    {
        public Guid Guid { get; set; }
        public Guid Customer { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Product { get; set; }
        public decimal? Dto { get; set; }
        public string? Obs { get; set; }
    }
}
