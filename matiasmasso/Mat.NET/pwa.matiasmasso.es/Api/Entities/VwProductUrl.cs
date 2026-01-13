using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Url segments per product
    /// </summary>
    public partial class VwProductUrl
    {
        public Guid Guid { get; set; }
        public string? BrandLang { get; set; }
        public string BrandSegment { get; set; } = null!;
        public bool BrandCanonical { get; set; }
        public string? DeptLang { get; set; }
        public string? DeptSegment { get; set; }
        public bool? DeptCanonical { get; set; }
        public string? CategoryLang { get; set; }
        public string? CategorySegment { get; set; }
        public bool? CategoryCanonical { get; set; }
        public string? SkuLang { get; set; }
        public string? SkuSegment { get; set; }
        public bool? SkuCanonical { get; set; }
        public bool IncludeDeptOnUrl { get; set; }
    }
}
