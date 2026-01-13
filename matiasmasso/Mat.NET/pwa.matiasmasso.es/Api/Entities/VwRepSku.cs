using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Agent product sku range, with sku names, excluding obsolets, accessories and spares
    /// </summary>
    public partial class VwRepSku
    {
        /// <summary>
        /// Product brand sort order
        /// </summary>
        public int BrandOrd { get; set; }
        /// <summary>
        /// Product category code (product, accessory, spare, Point of Sale materials...)
        /// </summary>
        public int CategoryCodi { get; set; }
        /// <summary>
        /// Product category sort order
        /// </summary>
        public short CategoryOrd { get; set; }
        /// <summary>
        /// Product sku name (Spanish)
        /// </summary>
        public string SkuNom { get; set; } = null!;
        /// <summary>
        /// Product sku name (Catalan, if different from Spanish)
        /// </summary>
        public string? SkuNomCat { get; set; }
        /// <summary>
        /// Product sku name (English, if different from Spanish)
        /// </summary>
        public string? SkuNomEng { get; set; }
        /// <summary>
        /// Product sku name (Portuguese, if different from Spanish)
        /// </summary>
        public string? SkuNomPor { get; set; }
        /// <summary>
        /// Product brand name
        /// </summary>
        public string BrandNom { get; set; } = null!;
        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryNom { get; set; } = null!;
        /// <summary>
        /// Product brand id; foreign key for Tpa table
        /// </summary>
        public Guid BrandGuid { get; set; }
        /// <summary>
        /// Product category id; foreign key for Stp table
        /// </summary>
        public Guid CategoryGuid { get; set; }
        /// <summary>
        /// Product sku Id; foreign key for Art table
        /// </summary>
        public Guid SkuGuid { get; set; }
        /// <summary>
        /// Rep id; foreign key for CliGral table
        /// </summary>
        public Guid Rep { get; set; }
    }
}
