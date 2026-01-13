using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Vehicles owned by the company
/// </summary>
public partial class VehicleFlotum
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company, foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Vehicle model, foreign key for Vehicle_Models table
    /// </summary>
    public Guid? ModelGuid { get; set; }

    /// <summary>
    /// Vehicle plate number
    /// </summary>
    public string? Matricula { get; set; }

    /// <summary>
    /// Vehicle frame number
    /// </summary>
    public string? Bastidor { get; set; }

    /// <summary>
    /// Driver, fhoreign key for CliGral table
    /// </summary>
    public Guid? ConductorGuid { get; set; }

    /// <summary>
    /// Company the vehicle was purchased to, foreign key to CliGral table
    /// </summary>
    public Guid? VenedorGuid { get; set; }

    /// <summary>
    /// Date of acquisition
    /// </summary>
    public DateOnly Alta { get; set; }

    /// <summary>
    /// Date of removal from inventory
    /// </summary>
    public DateOnly? Baixa { get; set; }

    /// <summary>
    /// Acquisition contract, foreign key from Contracts table
    /// </summary>
    public Guid? Contracte { get; set; }

    /// <summary>
    /// Insurance policy, foreign key to Contracts table
    /// </summary>
    public Guid? Insurance { get; set; }

    /// <summary>
    /// Picture of the vehicle
    /// </summary>
    public byte[]? Image { get; set; }

    public int? ImageMime { get; set; }

    /// <summary>
    /// True if private owned
    /// </summary>
    public bool Privat { get; set; }

    public virtual CliGral? Conductor { get; set; }

    public virtual Contract? ContracteNavigation { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual Contract? InsuranceNavigation { get; set; }

    public virtual VehicleModel? Model { get; set; }

    public virtual CliGral? Venedor { get; set; }
}
