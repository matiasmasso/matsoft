using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product filter targets
    /// </summary>
    public partial class VwFilterTarget
    {
        public Guid ParentProduct { get; set; }
        public Guid FilterItem { get; set; }
    }
}
