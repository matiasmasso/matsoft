using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Used to calculate recommended sale points per product on our website
    /// </summary>
    public partial class VwWebAtla
    {
        public Guid BrandGuid { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
        public string SkuRef { get; set; } = null!;
        public Guid? Proveidor { get; set; }
        public Guid CliGuid { get; set; }
        public decimal? Val { get; set; }
        public decimal? ValHistoric { get; set; }
        public DateTime? LastFch { get; set; }
    }
}
