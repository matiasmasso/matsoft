using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Warehouses, logistic centers
    /// </summary>
    public partial class Mgz
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string? Nom { get; set; }

        public virtual CliGral Gu { get; set; } = null!;
    }
}
