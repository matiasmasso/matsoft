using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Social security employees category groups
    /// </summary>
    public partial class SegSocialGrup
    {
        public SegSocialGrup()
        {
            StaffCategories = new HashSet<StaffCategory>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Group name
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual ICollection<StaffCategory> StaffCategories { get; set; }
    }
}
