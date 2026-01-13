using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Contracts
/// </summary>
public partial class Contract
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    public int? Emp { get; set; }

    /// <summary>
    /// Purpose code; foreign key for Contract_Codis
    /// </summary>
    public Guid? CodiGuid { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Nom { get; set; } = null!;

    /// <summary>
    /// Contract counterpart; foreign key for CliGral table
    /// </summary>
    public Guid? ContactGuid { get; set; }

    /// <summary>
    /// Contract number
    /// </summary>
    public string Num { get; set; } = null!;

    /// <summary>
    /// Contract effective date
    /// </summary>
    public DateOnly FchFrom { get; set; }

    /// <summary>
    /// Contract expiry date
    /// </summary>
    public DateOnly? FchTo { get; set; }

    public bool Privat { get; set; }

    /// <summary>
    /// Document; foreign key for Docfile table
    /// </summary>
    public string? Hash { get; set; }

    public virtual ContractCodi? Codi { get; set; }

    public virtual CliGral? Contact { get; set; }

    public virtual DocFile? HashNavigation { get; set; }

    public virtual ICollection<VehicleFlotum> VehicleFlotumContracteNavigations { get; set; } = new List<VehicleFlotum>();

    public virtual ICollection<VehicleFlotum> VehicleFlotumInsuranceNavigations { get; set; } = new List<VehicleFlotum>();
}
