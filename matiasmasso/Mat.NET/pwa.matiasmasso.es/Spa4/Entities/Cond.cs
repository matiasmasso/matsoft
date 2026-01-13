using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Commercial terms and conditions
    /// </summary>
    public partial class Cond
    {
        public Cond()
        {
            CondCapitols = new HashSet<CondCapitol>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }

        public virtual ICollection<CondCapitol> CondCapitols { get; set; }
    }
}
