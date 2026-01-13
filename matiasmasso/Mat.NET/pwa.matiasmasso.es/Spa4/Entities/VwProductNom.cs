using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// product names
    /// </summary>
    public partial class VwProductNom
    {
        public int Emp { get; set; }
        public Guid Guid { get; set; }
        public int Cod { get; set; }
        public Guid BrandGuid { get; set; }
        public string? BrandNom { get; set; }
        public Guid? Proveidor { get; set; }
        public Guid? CategoryGuid { get; set; }
        public int CategoryCodi { get; set; }
        public string? CategoryNom { get; set; }
        public Guid? SkuGuid { get; set; }
        public string? SkuNom { get; set; }
        public string? SkuNomLlarg { get; set; }
        public string? FullNom { get; set; }
        public Guid? DeptGuid { get; set; }
        public string? DeptNom { get; set; }
        public int Obsoleto { get; set; }
        public DateTime FchCreated { get; set; }
        public Guid? CnapGuid { get; set; }
    }
}
