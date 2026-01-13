using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class MediaResourceTarget
{
    public Guid Parent { get; set; }

    public Guid Target { get; set; }

    public virtual MediaResource ParentNavigation { get; set; } = null!;
}
