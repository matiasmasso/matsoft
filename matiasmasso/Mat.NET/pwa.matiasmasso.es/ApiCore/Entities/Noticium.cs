using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// News posts
/// </summary>
public partial class Noticium
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company; foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// News date
    /// </summary>
    public DateTime Fch { get; set; }

    /// <summary>
    /// Friendly segment of the landing page url
    /// </summary>
    public string? UrlFriendlySegment { get; set; }

    /// <summary>
    /// If the news relate to a specific brand, brand category or product, foreign key to either table Tpa, Stp or Art respectively
    /// </summary>
    public Guid? Brand { get; set; }

    /// <summary>
    /// Product classification; foreign key for Cnap table
    /// </summary>
    public Guid? Cnap { get; set; }

    /// <summary>
    /// True if visibility limited to professionals
    /// </summary>
    public bool Professional { get; set; }

    /// <summary>
    /// Hidden to web visitors if false
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// Enumerable DTONoticia.PriorityLevels (0.low, 1.high)
    /// </summary>
    public short Priority { get; set; }

    /// <summary>
    /// Featured image
    /// </summary>
    public byte[]? Image265x150 { get; set; }

    /// <summary>
    /// Enumerable DTONoticia.Srcs (0.news, 1.events...)
    /// </summary>
    public int Cod { get; set; }

    /// <summary>
    /// In case of events, start date
    /// </summary>
    public DateOnly? FchFrom { get; set; }

    /// <summary>
    /// In case of events, end date
    /// </summary>
    public DateOnly? FchTo { get; set; }

    /// <summary>
    /// Greographical area, if restricted to any
    /// </summary>
    public Guid? Location { get; set; }

    /// <summary>
    /// Date this post should be highlited from
    /// </summary>
    public DateOnly? DestacarFrom { get; set; }

    /// <summary>
    /// Date this post should no longer be highlited
    /// </summary>
    public DateOnly? DestacarTo { get; set; }

    /// <summary>
    /// Date and time this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Date and time this entry was last edited
    /// </summary>
    public DateTime FchLastEdited { get; set; }

    /// <summary>
    /// User who created this entry; foreign key for Email table
    /// </summary>
    public Guid? UsrCreated { get; set; }

    /// <summary>
    /// User who edited this entry for last time; foreign key for Email table
    /// </summary>
    public Guid? UsrLastEdited { get; set; }

    public virtual Cnap? CnapNavigation { get; set; }

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<Incentiu> Incentius { get; set; } = new List<Incentiu>();

    public virtual Email? UsrCreatedNavigation { get; set; }

    public virtual Email? UsrLastEditedNavigation { get; set; }

    public virtual ICollection<CategoriaDeNoticium> Categoria { get; set; } = new List<CategoriaDeNoticium>();

    public virtual ICollection<DistributionChannel> Channels { get; set; } = new List<DistributionChannel>();
}
