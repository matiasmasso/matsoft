using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class EdiversaRecadvHdr
{
    public Guid Guid { get; set; }

    public string? Bgm { get; set; }

    public DateTime? Dtm { get; set; }

    public string? NadBy { get; set; }

    public string? RffOn { get; set; }

    public string? RffDq { get; set; }
}
