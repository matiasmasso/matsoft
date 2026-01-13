using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Album
{
    public Guid Guid { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string? Name { get; set; }

    public Guid UsrCreated { get; set; }

    public DateTimeOffset FchCreated { get; set; }

    public virtual ICollection<AlbumItem> AlbumItems { get; set; } = new List<AlbumItem>();

    public virtual UserAccount UsrCreatedNavigation { get; set; } = null!;
}
