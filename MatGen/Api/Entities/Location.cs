using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Location
{
    public Guid Guid { get; set; }

    public Guid? Parent { get; set; }

    public string? Nom { get; set; }

    public string? NomLlarg { get; set; }

    public DateTime FchCreated { get; set; }
}
