using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product categories per department
    /// </summary>
    public partial class VwDeptCategory
    {
        public Guid Dept { get; set; }
        public Guid Category { get; set; }
    }
}
