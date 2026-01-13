using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class MediaResourceTarget
    {
        public Guid Target { get; set; }
        public string Hash { get; set; } = null!;

        public virtual MediaResource HashNavigation { get; set; } = null!;
    }
}
