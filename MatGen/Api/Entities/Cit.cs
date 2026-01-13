using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Cit
    {
        public Guid Guid { get; set; }
        public string? GoogleMaps { get; set; }
        public Guid? Parent { get; set; }
    }
}
