using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Commercial brands
    /// </summary>
    public partial class Tpa
    {
        public Tpa()
        {
            BrandAreas = new HashSet<BrandArea>();
            Depts = new HashSet<Dept>();
            SocialMediaWidgets = new HashSet<SocialMediaWidget>();
            Stps = new HashSet<Stp>();
            Webs = new HashSet<Web>();
            Aperturas = new HashSet<CliApertura>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company Id, Foreign key to Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Sort order in which to display the brand among the rest of the brands
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Provider Id; foreign key to CliPrv table
        /// </summary>
        public Guid? Proveidor { get; set; }
        /// <summary>
        /// Foreign key to Cnap product classification table (Codificación Normalizada para Articulos de Puericultura)
        /// </summary>
        public Guid? CnapGuid { get; set; }
        /// <summary>
        /// Country of Origin. Foreign key to Country table
        /// </summary>
        public Guid? MadeIn { get; set; }
        /// <summary>
        /// Default Customs code (NC, Nomenclatura Combinada) for products of this brand, if any
        /// </summary>
        public string? CodiMercancia { get; set; }
        /// <summary>
        /// Distribution policy: 0.Free (any sale point is allowed to distribute) 1.Restricted (distribution is limited to selected sale points)
        /// </summary>
        public int CodDist { get; set; }
        /// <summary>
        /// Color to graphically identify the brand on charts
        /// </summary>
        public int Color { get; set; }
        /// <summary>
        ///  Max Days from last order to be published as distributor
        /// </summary>
        public int? WebAtlasDeadline { get; set; }
        /// <summary>
        /// Max Days from last order to be published as raffle sale point
        /// </summary>
        public int? WebAtlasRafflesDeadline { get; set; }
        /// <summary>
        /// True if allowed to be displayed to consumers (on web site...)
        /// </summary>
        public bool WebEnabledConsumer { get; set; }
        /// <summary>
        /// True if allowed to be displayed to proffessionals (on price lists, order forms...)
        /// </summary>
        public bool WebEnabledPro { get; set; }
        /// <summary>
        /// True if allowed to display to consumers the list of official sale points for products of this brand
        /// </summary>
        public bool? ShowAtlas { get; set; }
        /// <summary>
        /// If true, url is built as brand/department/category/sku... else as brand/category/sku...
        /// </summary>
        public bool IncludeDeptOnUrl { get; set; }
        public bool EnLiquidacio { get; set; }
        /// <summary>
        /// true if the brand is no longer active
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Brand logo 150x48px
        /// </summary>
        public byte[]? Logo { get; set; }
        /// <summary>
        /// Official brand logo each distributor is allowed to publish on their website
        /// </summary>
        public byte[]? LogoDistribuidorOficial { get; set; }
        /// <summary>
        /// Date and time this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Last date and time this entry was edited
        /// </summary>
        public DateTime FchLastEdited { get; set; }

        public virtual Cnap? CnapGu { get; set; }
        public virtual CodisMercancium? CodiMercanciaNavigation { get; set; }
        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual Country? MadeInNavigation { get; set; }
        public virtual CliPrv? ProveidorNavigation { get; set; }
        public virtual ICollection<BrandArea> BrandAreas { get; set; }
        public virtual ICollection<Dept> Depts { get; set; }
        public virtual ICollection<SocialMediaWidget> SocialMediaWidgets { get; set; }
        public virtual ICollection<Stp> Stps { get; set; }
        public virtual ICollection<Web> Webs { get; set; }

        public virtual ICollection<CliApertura> Aperturas { get; set; }
    }
}
