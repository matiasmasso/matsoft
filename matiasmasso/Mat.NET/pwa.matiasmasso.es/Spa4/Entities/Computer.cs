using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class Computer
    {
        public Guid Guid { get; set; }
        public string? Nom { get; set; }
        public string? Text { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
    }
}
