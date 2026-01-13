using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contacts per distribuition channel
    /// </summary>
    public partial class VwContactChannel
    {
        public Guid Contact { get; set; }
        public Guid? Channel { get; set; }
    }
}
