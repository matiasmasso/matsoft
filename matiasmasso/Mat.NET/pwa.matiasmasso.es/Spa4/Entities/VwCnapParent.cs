using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Hierarchical product classification by functionality
    /// </summary>
    public partial class VwCnapParent
    {
        public Guid? Parent { get; set; }
        public Guid? Child { get; set; }
    }
}
