using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Employees
/// </summary>
public partial class CliStaff
{
    /// <summary>
    /// Primary key; foreign key for CliGral table
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Abr { get; set; } = null!;

    /// <summary>
    /// Social security number
    /// </summary>
    public string? NumSs { get; set; }

    /// <summary>
    /// Entry date
    /// </summary>
    public DateTime? Alta { get; set; }

    /// <summary>
    /// Termination date, if any
    /// </summary>
    public DateTime? Baja { get; set; }

    /// <summary>
    /// Birth date
    /// </summary>
    public DateTime? Nac { get; set; }

    /// <summary>
    /// Enumerable DTOEnums.Sexs (1.male, 2.female)
    /// </summary>
    public int Sex { get; set; }

    /// <summary>
    /// Social security category; foreign key for StaffCategory table
    /// </summary>
    public Guid? Categoria { get; set; }

    /// <summary>
    /// Employee position; foreign key for StaffPos table
    /// </summary>
    public Guid? StaffPos { get; set; }

    /// <summary>
    /// Employee picture
    /// </summary>
    public byte[]? Avatar { get; set; }

    public bool TeleTrabajo { get; set; }

    public virtual StaffCategory? CategoriaNavigation { get; set; }

    public virtual CliGral Gu { get; set; } = null!;

    public virtual ICollection<StaffHoliday> StaffHolidays { get; set; } = new List<StaffHoliday>();

    public virtual StaffPo? StaffPosNavigation { get; set; }

    public virtual ICollection<StaffSched> StaffScheds { get; set; } = new List<StaffSched>();
}
