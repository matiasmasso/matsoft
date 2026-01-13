using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class MenuItem
    {
        public Guid Guid { get; set; }
        public int? Page { get; set; }
        public Guid? Parent { get; set; }
        public int? Ord { get; set; }
        public int? Mode { get; set; }
        public string? Action { get; set; }
        public string? Ico { get; set; }
        public bool Private { get; set; }
    }
}
