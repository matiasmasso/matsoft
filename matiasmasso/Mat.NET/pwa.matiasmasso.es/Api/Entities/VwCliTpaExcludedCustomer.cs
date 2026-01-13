using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product ranges included/excluded per customer
    /// </summary>
    public partial class VwCliTpaExcludedCustomer
    {
        public Guid Customer { get; set; }
        public Guid Sku { get; set; }
    }
}
