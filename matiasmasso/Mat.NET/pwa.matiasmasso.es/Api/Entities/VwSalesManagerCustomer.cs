using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customers on each sales manager portfolio
    /// </summary>
    public partial class VwSalesManagerCustomer
    {
        public Guid SalesManager { get; set; }
        public Guid Customer { get; set; }
    }
}
