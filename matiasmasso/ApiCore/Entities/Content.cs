using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Brand content for websites, Html formatted. Text stored in LangText table
/// </summary>
public partial class Content
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Visible if true
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }
}
