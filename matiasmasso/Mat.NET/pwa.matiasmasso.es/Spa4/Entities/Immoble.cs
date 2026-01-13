using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Real Estate properties (buildings, offices, appartments...)
    /// </summary>
    public partial class Immoble
    {
        public Immoble()
        {
            InventariItems = new HashSet<InventariItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company Id, Fopreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Nickname for this record
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Physical address (street, number, flat...)
        /// </summary>
        public string Adr { get; set; } = null!;
        /// <summary>
        /// Postal code, foreign key to Zip table
        /// </summary>
        public Guid ZipGuid { get; set; }
        public int? Titularitat { get; set; }
        public decimal? Part { get; set; }
        /// <summary>
        /// Land registry reference
        /// </summary>
        public string? Cadastre { get; set; }
        /// <summary>
        /// Date it was acquired
        /// </summary>
        public DateTime? FchFrom { get; set; }
        /// <summary>
        /// Date it was sold
        /// </summary>
        public DateTime? FchTo { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual Zip ZipGu { get; set; } = null!;
        public virtual ICollection<InventariItem> InventariItems { get; set; }
    }
}
