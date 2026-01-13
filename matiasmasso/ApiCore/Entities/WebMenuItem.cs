using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Website menu items
/// </summary>
public partial class WebMenuItem
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Menu group under which this item is displayed; foreign key for WebMenuGroups table
    /// </summary>
    public Guid? WebMenuGroup { get; set; }

    /// <summary>
    /// Link to navigate when the item is clicked
    /// </summary>
    public string Url { get; set; } = null!;

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

    /// <summary>
    /// Sort order
    /// </summary>
    public int Ord { get; set; }

    /// <summary>
    /// Displayed if true
    /// </summary>
    public bool? Actiu { get; set; }

    public virtual WebMenuGroup? WebMenuGroupNavigation { get; set; }

    public virtual ICollection<UsrRol> Rols { get; set; } = new List<UsrRol>();
}
