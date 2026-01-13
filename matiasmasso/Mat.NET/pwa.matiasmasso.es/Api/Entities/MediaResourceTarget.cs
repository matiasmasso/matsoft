using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class MediaResourceTarget
    {
        public Guid Target { get; set; }
        public string Hash { get; set; } = null!;

        public virtual MediaResource HashNavigation { get; set; } = null!;
    }
}
