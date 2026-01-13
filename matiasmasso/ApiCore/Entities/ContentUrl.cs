using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class ContentUrl
{
    public Guid Target { get; set; }

    public string Lang { get; set; } = null!;

    public string UrlSegment { get; set; } = null!;
}
