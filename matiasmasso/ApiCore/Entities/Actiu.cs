using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Actiu
{
    public Guid Guid { get; set; }

    public string? Nom { get; set; }

    public Guid? Cta { get; set; }

    public virtual ICollection<ActiuMov> ActiuMovs { get; set; } = new List<ActiuMov>();

    public virtual PgcCtum? CtaNavigation { get; set; }
}
