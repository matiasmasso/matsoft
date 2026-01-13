using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customers whom to invoice deliveries to other destinations
    /// </summary>
    public partial class VwCcxOrMe
    {
        public Guid Guid { get; set; }
        public Guid? Ccx { get; set; }
    }
}
