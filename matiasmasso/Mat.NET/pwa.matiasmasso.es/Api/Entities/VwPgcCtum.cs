using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Accounts names and codes
    /// </summary>
    public partial class VwPgcCtum
    {
        public int YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int Cod { get; set; }
        public Guid CtaGuid { get; set; }
        public string Id { get; set; } = null!;
        public string Esp { get; set; } = null!;
        public string Cat { get; set; } = null!;
        public string Eng { get; set; } = null!;
    }
}
