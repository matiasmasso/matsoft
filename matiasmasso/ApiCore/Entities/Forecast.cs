using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Order forecasts
/// </summary>
public partial class Forecast
{
    /// <summary>
    /// Product; foreign key for Art table
    /// </summary>
    public Guid Sku { get; set; }

    /// <summary>
    /// Forecast year
    /// </summary>
    public int Yea { get; set; }

    /// <summary>
    /// Forecast month
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Date this entry was created; just the last entry for same year/month is valid
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// If null, it means it is our own sales forecast; otherelse it is customer&apos;s forecast; foreign key for CliGral table
    /// </summary>
    public Guid? Customer { get; set; }

    /// <summary>
    /// Units forecasted
    /// </summary>
    public int Qty { get; set; }

    /// <summary>
    /// User who created this entry; foreign key for Email table
    /// </summary>
    public Guid? UsrCreated { get; set; }

    public virtual CliGral? CustomerNavigation { get; set; }

    public virtual Art SkuNavigation { get; set; } = null!;

    public virtual Email? UsrCreatedNavigation { get; set; }
}
