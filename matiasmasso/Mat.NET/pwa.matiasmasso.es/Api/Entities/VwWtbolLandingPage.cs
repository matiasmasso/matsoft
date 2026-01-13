using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product landing pages to forward visitors to purchase on customer e-commerces
    /// </summary>
    public partial class VwWtbolLandingPage
    {
        public Guid Site { get; set; }
        public Guid LandingPage { get; set; }
        public string Url { get; set; } = null!;
        public Guid Product { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public DateTime? FchCreated { get; set; }
        public DateTime? StockFchCreated { get; set; }
        public DateTime LandingPageFchCreated { get; set; }
        public int Status { get; set; }
        public DateTime? FchStatus { get; set; }
    }
}
