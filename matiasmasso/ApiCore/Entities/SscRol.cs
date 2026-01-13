using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// User rols available for each subscription
/// </summary>
public partial class SscRol
{
    /// <summary>
    /// Subscription; foreign key for Ssc table
    /// </summary>
    public Guid SscGuid { get; set; }

    /// <summary>
    /// Enumerable DTORol.Ids
    /// </summary>
    public int Rol { get; set; }

    public virtual Ssc Ssc { get; set; } = null!;
}
