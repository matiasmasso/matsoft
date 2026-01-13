using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product categories names
    /// </summary>
    public partial class VwCategoryNom
    {
        public Guid CategoryGuid { get; set; }
        public short Emp { get; set; }
        public string? CategoryNom { get; set; }
        public string? CategoryNomCat { get; set; }
        public string? CategoryNomEng { get; set; }
        public string? CategoryNomPor { get; set; }
        public int Codi { get; set; }
        public short CategoryOrd { get; set; }
        public Guid? CnapGuid { get; set; }
        public Guid BrandGuid { get; set; }
        public int BrandOrd { get; set; }
        public string? BrandNom { get; set; }
        public string? BrandNomCat { get; set; }
        public string? BrandNomEng { get; set; }
        public string? BrandNomPor { get; set; }
        public int CodDist { get; set; }
        public Guid? Proveidor { get; set; }
        public bool BrandObsoleto { get; set; }
        public int Obsoleto { get; set; }
        public int WebEnabledConsumer { get; set; }
        public bool CategoryIsBundle { get; set; }
    }
}
