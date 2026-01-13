using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// User default values
/// </summary>
public partial class UserDefault
{
    /// <summary>
    /// User; foreign key for Email table
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Purpose of the value; Enumerable DTOUserDefault.Cods
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// User value for this purpose code
    /// </summary>
    public string Value { get; set; } = null!;

    public virtual Email User { get; set; } = null!;
}
