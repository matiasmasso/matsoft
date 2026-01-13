using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwProductBreadcrumb
    {
        public Guid Guid { get; set; }
        public bool Obsoleto { get; set; }
        public string? BrandNomEsp { get; set; }
        public string? BrandNomCat { get; set; }
        public string? BrandNomEng { get; set; }
        public string? BrandNomPor { get; set; }
        public string? DeptNomEsp { get; set; }
        public string? DeptNomCat { get; set; }
        public string? DeptNomEng { get; set; }
        public string? DeptNomPor { get; set; }
        public string? CategoryNomEsp { get; set; }
        public string? CategoryNomCat { get; set; }
        public string? CategoryNomEng { get; set; }
        public string? CategoryNomPor { get; set; }
        public string? SkuNomEsp { get; set; }
        public string? SkuNomCat { get; set; }
        public string? SkuNomEng { get; set; }
        public string? SkuNomPor { get; set; }
        public string? BrandUrlEsp { get; set; }
        public string? BrandUrlCat { get; set; }
        public string? BrandUrlEng { get; set; }
        public string? BrandUrlPor { get; set; }
        public string? DeptUrlEsp { get; set; }
        public string? DeptUrlCat { get; set; }
        public string? DeptUrlEng { get; set; }
        public string? DeptUrlPor { get; set; }
        public string? CategoryUrlEsp { get; set; }
        public string? CategoryUrlCat { get; set; }
        public string? CategoryUrlEng { get; set; }
        public string? CategoryUrlPor { get; set; }
        public string? SkuUrlEsp { get; set; }
        public string? SkuUrlCat { get; set; }
        public string? SkuUrlEng { get; set; }
        public string? SkuUrlPor { get; set; }
        public bool? IncludeDeptOnUrl { get; set; }
        public Guid Brand { get; set; }
        public Guid? Dept { get; set; }
        public Guid? Category { get; set; }
        public Guid? Sku { get; set; }
        public int Cod { get; set; }
    }
}
