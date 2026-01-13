using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class AlbumItem
{
    public Guid Guid { get; set; }

    public Guid Album { get; set; }

    public int SortOrder { get; set; }

    public string Hash { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public int? Size { get; set; }

    public long? DurationSeconds { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Guid UsrCreated { get; set; }

    public DateTimeOffset FchCreated { get; set; }

    public virtual Album AlbumNavigation { get; set; } = null!;

    public virtual UserAccount UsrCreatedNavigation { get; set; } = null!;
}
