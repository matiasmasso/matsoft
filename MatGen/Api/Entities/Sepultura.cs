using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Sepultura
    {
        public Guid Guid { get; set; }
        public Guid? Town { get; set; }
    }
}
