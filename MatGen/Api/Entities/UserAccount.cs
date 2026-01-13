using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class UserAccount
{
    public Guid Guid { get; set; }

    public string? EmailAddress { get; set; }

    public string? Hash { get; set; }

    public string? Nickname { get; set; }

    public int Rol { get; set; }

    public Guid? RootPerson { get; set; }

    public virtual ICollection<AlbumItem> AlbumItems { get; set; } = new List<AlbumItem>();

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
