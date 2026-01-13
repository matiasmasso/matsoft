using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Real Estate properties (buildings, offices, appartments...)
/// </summary>
public partial class Immoble
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company Id, Fopreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Nickname for this record
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Physical address (street, number, flat...)
    /// </summary>
    public string? Adr { get; set; }

    /// <summary>
    /// Postal code, foreign key to Zip table
    /// </summary>
    public Guid? ZipGuid { get; set; }

    public int? Titularitat { get; set; }

    public decimal? Part { get; set; }

    /// <summary>
    /// Land registry reference
    /// </summary>
    public string? Cadastre { get; set; }

    public string? Registre { get; set; }

    /// <summary>
    /// Date it was acquired
    /// </summary>
    public DateOnly? FchFrom { get; set; }

    /// <summary>
    /// Date it was sold
    /// </summary>
    public DateOnly? FchTo { get; set; }

    public decimal? Superficie { get; set; }

    public string? Obs { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<InventariItem> InventariItems { get; set; } = new List<InventariItem>();

    public virtual ICollection<Lloguer> Lloguers { get; set; } = new List<Lloguer>();

    public virtual Zip? Zip { get; set; }
}
