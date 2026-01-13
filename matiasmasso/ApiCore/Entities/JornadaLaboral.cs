using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class JornadaLaboral
{
    public Guid Guid { get; set; }

    public Guid Staff { get; set; }

    public DateTime FchFrom { get; set; }

    public DateTime? FchTo { get; set; }
}
