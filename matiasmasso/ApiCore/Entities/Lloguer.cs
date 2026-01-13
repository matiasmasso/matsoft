using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Lloguer
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public Guid Customer { get; set; }

    public Guid? Immoble { get; set; }

    public Guid? Contract { get; set; }

    public int? Cod { get; set; }

    public decimal? Base { get; set; }

    public bool? Iva { get; set; }

    public bool? Irpf { get; set; }

    public DateOnly? FchFrom { get; set; }

    public DateOnly? FchTo { get; set; }

    public virtual Contract? ContractNavigation { get; set; }

    public virtual CliGral CustomerNavigation { get; set; } = null!;

    public virtual Immoble? ImmobleNavigation { get; set; }

    public virtual ICollection<Fra> Invoices { get; set; } = new List<Fra>();
}
