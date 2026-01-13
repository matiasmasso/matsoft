using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwNav
    {
        public Guid Guid { get; set; }
        public Guid? Parent { get; set; }
        public int Ord { get; set; }
        public int Mode { get; set; }
        public string? Action { get; set; }
        public string? IcoSmall { get; set; }
        public string? IcoBig { get; set; }
        public int Rol { get; set; }
        public int Emp { get; set; }
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }
    }
}
