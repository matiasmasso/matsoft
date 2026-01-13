using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Employees holidays
    /// </summary>
    public partial class StaffHoliday
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Employee, if specific for certain employees; foreign key for CliGral table
        /// </summary>
        public Guid? Staff { get; set; }
        /// <summary>
        /// Justification. Enumerable DTOStaff.Cods
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Date starting holidays
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// Date ending holidays
        /// </summary>
        public DateTime FchTo { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }

        public virtual CliStaff? StaffNavigation { get; set; }
    }
}
