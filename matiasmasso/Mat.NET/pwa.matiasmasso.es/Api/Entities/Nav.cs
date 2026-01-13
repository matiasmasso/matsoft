using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Nav
    {
        public Nav()
        {
            InverseParentNavigation = new HashSet<Nav>();
            NavRols = new HashSet<NavRol>();
        }

        public Guid Guid { get; set; }
        public Guid? Parent { get; set; }
        public int Ord { get; set; }
        public int Mode { get; set; }
        public string? Action { get; set; }
        public string? IcoSmall { get; set; }
        public string? IcoBig { get; set; }

        public virtual Nav? ParentNavigation { get; set; }
        public virtual ICollection<Nav> InverseParentNavigation { get; set; }
        public virtual ICollection<NavRol> NavRols { get; set; }
    }
}
