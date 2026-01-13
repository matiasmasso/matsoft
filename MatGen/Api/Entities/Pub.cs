using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Pub
{
    public Guid Guid { get; set; }

    public string? Nom { get; set; }

    public DateTime FchCreated { get; set; }

    public string? Hash { get; set; }

    public string? Asin { get; set; }

    public virtual DocFile? HashNavigation { get; set; }
}
