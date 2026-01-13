using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product default urls
    /// </summary>
    public partial class VwProductDefaultUrl
    {
        public Guid Guid { get; set; }
        public string? BrandLang { get; set; }
        public string BrandSegment { get; set; } = null!;
        public string? DeptLang { get; set; }
        public string? DeptSegment { get; set; }
        public string? CategoryLang { get; set; }
        public string? CategorySegment { get; set; }
        public string? SkuLang { get; set; }
        public string? SkuSegment { get; set; }
    }
}
