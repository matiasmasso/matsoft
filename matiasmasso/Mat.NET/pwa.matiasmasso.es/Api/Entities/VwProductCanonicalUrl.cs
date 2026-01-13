using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product canonical url
    /// </summary>
    public partial class VwProductCanonicalUrl
    {
        public Guid Guid { get; set; }
        public string? BrandCat { get; set; }
        public string? BrandEng { get; set; }
        public string? BrandPor { get; set; }
        public string? BrandEsp { get; set; }
        public string? DeptCat { get; set; }
        public string? DeptEng { get; set; }
        public string? DeptPor { get; set; }
        public string? DeptEsp { get; set; }
        public string? CategoryCat { get; set; }
        public string? CategoryEng { get; set; }
        public string? CategoryPor { get; set; }
        public string? CategoryEsp { get; set; }
        public string? SkuCat { get; set; }
        public string? SkuEng { get; set; }
        public string? SkuPor { get; set; }
        public string? SkuEsp { get; set; }
    }
}
