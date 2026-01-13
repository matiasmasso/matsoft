using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Products included on each product plugin
    /// </summary>
    public partial class ProductPluginItem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table ProductPlugin
        /// </summary>
        public Guid Plugin { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Foreign key to either product brand Tpa table, product category Stp table or product sku Art table
        /// </summary>
        public Guid Product { get; set; }
        /// <summary>
        /// Product name, Spanish language
        /// </summary>
        public string? NomEsp { get; set; }
        /// <summary>
        /// Product name, Catalan language
        /// </summary>
        public string? NomCat { get; set; }
        /// <summary>
        /// Product name, English language
        /// </summary>
        public string? NomEng { get; set; }
        /// <summary>
        /// Product name, Portuguese language
        /// </summary>
        public string? NomPor { get; set; }

        public virtual ProductPlugin PluginNavigation { get; set; } = null!;
    }
}
