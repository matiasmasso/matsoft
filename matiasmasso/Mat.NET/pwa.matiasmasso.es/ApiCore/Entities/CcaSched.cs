using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class CcaSched
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public string? Concept { get; set; }

    public int? Ccd { get; set; }

    public Guid? Projecte { get; set; }

    public DateOnly? FchFrom { get; set; }

    public DateOnly? FchTo { get; set; }

    public DateTime? LastTime { get; set; }

    /// <summary>
    /// Frequency mode (monthly, weekly, daily...)
    /// </summary>
    public int? FreqMod { get; set; }

    /// <summary>
    /// Frequency due (day of month, day of week...)
    /// </summary>
    public int? FreqDue { get; set; }

    public virtual ICollection<CcaSchedItem> CcaSchedItems { get; set; } = new List<CcaSchedItem>();
}
