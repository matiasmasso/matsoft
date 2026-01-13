using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Keywords from news or blog posts
/// </summary>
public partial class Keyword
{
    /// <summary>
    /// Blog post or News post
    /// </summary>
    public Guid Target { get; set; }

    /// <summary>
    /// keyword text
    /// </summary>
    public string Value { get; set; } = null!;
}
