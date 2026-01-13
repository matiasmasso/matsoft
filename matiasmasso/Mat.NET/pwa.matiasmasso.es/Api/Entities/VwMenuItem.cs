using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwMenuItem
    {
        public Guid Guid { get; set; }
        public int? Page { get; set; }
        public Guid? Parent { get; set; }
        public int? Ord { get; set; }
        public int? Mode { get; set; }
        public string? Action { get; set; }
        public string? Ico { get; set; }
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }
        public int? Rol { get; set; }
        public bool Private { get; set; }
    }
}
