using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CustomerInvoiceTemplate
{
    public Guid Guid { get; set; }

    public string? Tag { get; set; }

    public int? Emp { get; set; }

    public Guid? Customer { get; set; }

    public Guid? CtaDebit { get; set; }

    public Guid? CtaCredit { get; set; }

    public string? Lang { get; set; }

    public string? Cur { get; set; }

    public decimal? IvaPct { get; set; }

    public decimal? IrpfPct { get; set; }

    public string? VfConcept { get; set; }

    public string? VfTaxScheme { get; set; }

    public string? VfTaxType { get; set; }

    public string? VfTaxException { get; set; }

    public virtual ICollection<CustomerInvoiceTemplateItem> CustomerInvoiceTemplateItems { get; set; } = new List<CustomerInvoiceTemplateItem>();
}
