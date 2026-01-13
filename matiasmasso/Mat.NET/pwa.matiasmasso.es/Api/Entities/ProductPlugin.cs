using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Website product plugins to include in Html posts
    /// </summary>
    public partial class ProductPlugin
    {
        public ProductPlugin()
        {
            ProductPluginItems = new HashSet<ProductPluginItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Plugin caption
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Plugin product, if any. Foreign key to either product brand Tpa table, product category Styp table or product sku Art table
        /// </summary>
        public Guid? Product { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who created this entry; foreign key for Email table
        /// </summary>
        public Guid? UsrCreated { get; set; }
        /// <summary>
        /// Date this entry was edited for last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who edited this entry for last time
        /// </summary>
        public Guid? UsrLastEdited { get; set; }

        public virtual Email? UsrCreatedNavigation { get; set; }
        public virtual Email? UsrLastEditedNavigation { get; set; }
        public virtual ICollection<ProductPluginItem> ProductPluginItems { get; set; }
    }
}
