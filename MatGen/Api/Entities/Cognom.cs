using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Cognom
{
    public Guid Guid { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
