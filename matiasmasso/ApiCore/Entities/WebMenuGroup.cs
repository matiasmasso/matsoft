using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Menu groups under which are display the different menu items
/// </summary>
public partial class WebMenuGroup
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public int Ord { get; set; }

    /// <summary>
    /// Caption, Spanish language
    /// </summary>
    public string? Esp { get; set; }

    /// <summary>
    /// Caption, Catalan language
    /// </summary>
    public string? Cat { get; set; }

    /// <summary>
    /// Caption, English language
    /// </summary>
    public string? Eng { get; set; }

    /// <summary>
    /// Caption, Portuguese language
    /// </summary>
    public string? Por { get; set; }

    public virtual ICollection<WebMenuItem> WebMenuItems { get; set; } = new List<WebMenuItem>();
}
