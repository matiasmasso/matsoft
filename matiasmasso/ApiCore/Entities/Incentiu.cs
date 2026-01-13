using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Commercial promotions
/// </summary>
public partial class Incentiu
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Start date
    /// </summary>
    public DateTime FchFrom { get; set; }

    /// <summary>
    /// Termination date
    /// </summary>
    public DateTime? FchTo { get; set; }

    /// <summary>
    /// Thumbnail image to ilustrate it on a web page list
    /// </summary>
    public byte[]? Thumbnail { get; set; }

    /// <summary>
    /// If true, the promotion is only b¡valid for product in stock
    /// </summary>
    public bool OnlyInStk { get; set; }

    /// <summary>
    /// Product range, foreign key to either brand Tpa table, product category Stp table or product Sku Art table
    /// </summary>
    public Guid? Product { get; set; }

    /// <summary>
    /// Enumerable DTOIncentiu.Cods (1.discount, 2.free units)
    /// </summary>
    public short Cod { get; set; }

    /// <summary>
    /// In case the promotion is link to a registered event
    /// </summary>
    public Guid? Evento { get; set; }

    /// <summary>
    /// True if visible to customers
    /// </summary>
    public bool? CliVisible { get; set; }

    /// <summary>
    /// True if visible to sales agents
    /// </summary>
    public bool? RepVisible { get; set; }

    /// <summary>
    /// Free text to explain manufacturer contribution
    /// </summary>
    public string? ManufacturerContribution { get; set; }

    public virtual Noticium? EventoNavigation { get; set; }

    public virtual ICollection<IncentiuProduct> IncentiuProducts { get; set; } = new List<IncentiuProduct>();

    public virtual ICollection<IncentiuQtyDto> IncentiuQtyDtos { get; set; } = new List<IncentiuQtyDto>();

    public virtual ICollection<Pdc> Pdcs { get; set; } = new List<Pdc>();

    public virtual ICollection<DistributionChannel> DistributionChannels { get; set; } = new List<DistributionChannel>();
}
