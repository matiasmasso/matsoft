using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Fix discount some customers may have granted in certain product ranges
    /// </summary>
    public partial class CliDto
    {
        /// <summary>
        /// Foreign key for CliGral table
        /// </summary>
        public Guid Customer { get; set; }
        /// <summary>
        /// Product range. Foreign key for either Tpa, Stp or Art tables
        /// </summary>
        public Guid Brand { get; set; }
        /// <summary>
        /// Granted discount
        /// </summary>
        public decimal Dto { get; set; }

        public virtual CliGral CustomerNavigation { get; set; } = null!;
    }
}
