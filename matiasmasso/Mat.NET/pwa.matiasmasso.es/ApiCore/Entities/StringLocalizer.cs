using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class StringLocalizer
{
    public string StringKey { get; set; } = null!;

    public string Lang { get; set; } = null!;

    public string Value { get; set; } = null!;
}
