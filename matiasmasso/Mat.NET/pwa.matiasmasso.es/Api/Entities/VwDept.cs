using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwDept
    {
        public Guid Guid { get; set; }
        public Guid Brand { get; set; }
        public int Ord { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchLastEdited { get; set; }
        public bool Obsoleto { get; set; }
        public int Src { get; set; }
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }
        public int Emp { get; set; }
    }
}
