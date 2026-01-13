using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CustomerInvoiceTemplateItem
{
    public Guid Template { get; set; }

    public int Cod { get; set; }

    public int Lin { get; set; }

    public string? Concept { get; set; }

    public decimal? Price { get; set; }

    public virtual CustomerInvoiceTemplate TemplateNavigation { get; set; } = null!;
}
