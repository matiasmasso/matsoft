using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwEdiInvrptHeader
    {
        public Guid? Holding { get; set; }
        public Guid Guid { get; set; }
        public string? DocNum { get; set; }
        public DateTime Fch { get; set; }
        public DateTime FchCreated { get; set; }
        public string Nadms { get; set; } = null!;
        public Guid Nadmsguid { get; set; }
        public string Nadgy { get; set; } = null!;
        public Guid? Nadgyguid { get; set; }
        public string? Nadgyref { get; set; }
    }
}
