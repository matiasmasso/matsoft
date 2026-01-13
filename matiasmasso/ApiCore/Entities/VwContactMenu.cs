using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwContactMenu
{
    public Guid Guid { get; set; }

    public byte IsObsoleto { get; set; }

    public int IsClient { get; set; }

    public int IsProveidor { get; set; }

    public int IsRep { get; set; }

    public int IsStaff { get; set; }

    public int IsBanc { get; set; }

    public int IsTransportista { get; set; }

    public int IsInsolvent { get; set; }
}
