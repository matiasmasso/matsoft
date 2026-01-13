using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Credit limit per customer
    /// </summary>
    public partial class VwCustomerCredit
    {
        public Guid CliGuid { get; set; }
        public decimal? Eur { get; set; }
        public DateTime FchCreated { get; set; }
        public string? Obs { get; set; }
    }
}
