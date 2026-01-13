using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Nav
{
    public Guid Guid { get; set; }

    public Guid? Parent { get; set; }

    public int Ord { get; set; }

    public int Mode { get; set; }

    public string? Action { get; set; }

    public string? IcoSmall { get; set; }

    public string? IcoBig { get; set; }

    public virtual ICollection<Nav> InverseParentNavigation { get; set; } = new List<Nav>();

    public virtual ICollection<NavRol> NavRols { get; set; } = new List<NavRol>();

    public virtual Nav? ParentNavigation { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Emp> Emps { get; set; } = new List<Emp>();
}
