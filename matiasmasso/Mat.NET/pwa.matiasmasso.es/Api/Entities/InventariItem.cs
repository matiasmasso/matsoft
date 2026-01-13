using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Inventory items
    /// </summary>
    public partial class InventariItem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Property Id, foreign key to Immoble table
        /// </summary>
        public Guid? Immoble { get; set; }
        /// <summary>
        /// Inventory item friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }

        public virtual Immoble? ImmobleNavigation { get; set; }
    }
}
