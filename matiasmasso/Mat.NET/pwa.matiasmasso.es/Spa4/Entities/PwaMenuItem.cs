using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class PwaMenuItem
    {
        public Guid Guid { get; set; }
        public Guid Parent { get; set; }
        public int Mode { get; set; }
        public string? Action { get; set; }
    }
}
