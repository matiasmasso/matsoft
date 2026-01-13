using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Entitat
    {
        public Guid Guid { get; set; }
        public Guid? Cit { get; set; }
        public int Codi { get; set; }
        public int IdOld { get; set; }
    }
}
