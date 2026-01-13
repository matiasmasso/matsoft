using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Social security employee categories
    /// </summary>
    public partial class StaffCategory
    {
        public StaffCategory()
        {
            CliStaffs = new HashSet<CliStaff>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Group; foreign key for SegSocialGrups table
        /// </summary>
        public Guid? SegSocialGrup { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Category name
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual SegSocialGrup? SegSocialGrupNavigation { get; set; }
        public virtual ICollection<CliStaff> CliStaffs { get; set; }
    }
}
