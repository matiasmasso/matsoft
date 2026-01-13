using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class PwaMenuItem
    {
        public Guid Guid { get; set; }
        public Guid Parent { get; set; }
        public int Mode { get; set; }
        public string? Action { get; set; }
    }
}
