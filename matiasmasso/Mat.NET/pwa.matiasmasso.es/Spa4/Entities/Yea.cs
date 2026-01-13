using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Assigns a Guid number to each combination of Company and year
    /// </summary>
    public partial class Yea
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        public int Yea1 { get; set; }
        /// <summary>
        /// Company. Foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
    }
}
