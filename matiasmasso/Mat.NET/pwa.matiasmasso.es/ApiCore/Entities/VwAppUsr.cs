using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwAppUsr
{
    public DateTime? FirstFch { get; set; }

    public DateTime? LastFch { get; set; }

    public Guid? DeviceId { get; set; }

    public string? DeviceModel { get; set; }

    public string? AppVersion { get; set; }

    public string? Usr { get; set; }
}
