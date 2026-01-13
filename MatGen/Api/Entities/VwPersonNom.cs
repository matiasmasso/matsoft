using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwPersonNom
    {
        public Guid Guid { get; set; }
        public string? FchFrom { get; set; }
        public string? FchTo { get; set; }
        public string Nom { get; set; } = null!;
        public short Sex { get; set; }
    }
}
