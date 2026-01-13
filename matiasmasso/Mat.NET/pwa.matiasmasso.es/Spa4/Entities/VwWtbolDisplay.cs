using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwWtbolDisplay
    {
        public Guid Product { get; set; }
        public Guid Site { get; set; }
        public DateTime? FchStatus { get; set; }
        public string? Nom { get; set; }
        public string Web { get; set; } = null!;
        public string? Url { get; set; }
        public Guid? LandingPage { get; set; }
        public int? SiteStock { get; set; }
        public DateTime? FchLastStocks { get; set; }
        public decimal? Inventory { get; set; }
    }
}
