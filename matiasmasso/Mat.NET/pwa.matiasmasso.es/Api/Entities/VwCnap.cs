using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product classification by functionality
    /// </summary>
    public partial class VwCnap
    {
        public Guid Guid { get; set; }
        public Guid? Parent { get; set; }
        public string Id { get; set; } = null!;
        public string? Tags { get; set; }
        public string? ShortNomEsp { get; set; }
        public string? ShortNomCat { get; set; }
        public string? ShortNomEng { get; set; }
        public string? ShortNomPor { get; set; }
        public string? LongNomEsp { get; set; }
        public string? LongNomCat { get; set; }
        public string? LongNomEng { get; set; }
        public string? LongNomPor { get; set; }
    }
}
