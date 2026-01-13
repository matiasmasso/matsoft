using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Menu
    {
        public Guid Guid { get; set; }
        public string Caption { get; set; } = null!;
        public string Url { get; set; } = null!;
        public Guid? Parent { get; set; }
        public int? Ord { get; set; }
    }
}
