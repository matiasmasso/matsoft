using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Canonical Url segments per product
    /// </summary>
    public partial class VwProductUrlCanonical
    {
        public Guid Guid { get; set; }
        public string? UrlBrandEsp { get; set; }
        public string? UrlBrandCat { get; set; }
        public string? UrlBrandEng { get; set; }
        public string? UrlBrandPor { get; set; }
        public string? UrlDeptEsp { get; set; }
        public string? UrlDeptCat { get; set; }
        public string? UrlDeptEng { get; set; }
        public string? UrlDeptPor { get; set; }
        public string? UrlCategoryEsp { get; set; }
        public string? UrlCategoryCat { get; set; }
        public string? UrlCategoryEng { get; set; }
        public string? UrlCategoryPor { get; set; }
        public string? UrlSkuEsp { get; set; }
        public string? UrlSkuCat { get; set; }
        public string? UrlSkuEng { get; set; }
        public string? UrlSkuPor { get; set; }
        public bool IncludeDeptOnUrl { get; set; }
    }
}
