using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class DocSrc
{
    public Guid Guid { get; set; }

    public string Nom { get; set; } = null!;

    public string? NomLlarg { get; set; }

    public string? Obs { get; set; }

    public Guid? Parent { get; set; }

    public int Ord { get; set; }

    public Guid? Repo { get; set; }

    public DateTime FchCreated { get; set; }

    public string? Hash { get; set; }

    public string? Url { get; set; }

    public string? Asin { get; set; }

    public virtual DocFile? HashNavigation { get; set; }

    public virtual Repo? RepoNavigation { get; set; }
}
