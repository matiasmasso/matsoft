using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Profession
{
    public Guid Guid { get; set; }

    public string Nom { get; set; } = null!;

    public string? Llati { get; set; }

    public string? Obs { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
