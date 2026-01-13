using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contact logos
    /// </summary>
    public partial class Clx
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Logo or avatar, 48 pixels height
        /// </summary>
        public byte[]? Img48 { get; set; }

        public virtual CliGral Gu { get; set; } = null!;
    }
}
