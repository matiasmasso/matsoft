using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwEciPlantillaModLog
    {
        public int SkuId { get; set; }
        public string? Ean13 { get; set; }
        public string? SkuNomLlargEsp { get; set; }
        public string Descatalogado { get; set; } = null!;
        public string Rotura { get; set; } = null!;
        public DateTime Fch { get; set; }
    }
}
