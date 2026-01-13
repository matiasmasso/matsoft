using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class ContentUrl
    {
        public Guid Target { get; set; }
        public string Lang { get; set; } = null!;
        public string UrlSegment { get; set; } = null!;
    }
}
