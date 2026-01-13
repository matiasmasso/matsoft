using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product categories for each commercial brand
    /// </summary>
    public partial class Stp
    {
        public Stp()
        {
            Arts = new HashSet<Art>();
            PremiumProducts = new HashSet<PremiumProduct>();
            Webs = new HashSet<Web>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Commercial brand to which the category belongs. Foreign key to Stp table
        /// </summary>
        public Guid Brand { get; set; }
        /// <summary>
        /// Index where to display the category when sorted order applies
        /// </summary>
        public short Ord { get; set; }
        /// <summary>
        /// 0.Standard product, 1.Accessory, 2.Spare part, 3.POS Marketing materials, 4.Others
        /// </summary>
        public int Codi { get; set; }
        /// <summary>
        /// Color to graphically identify the category on charts
        /// </summary>
        public int Color { get; set; }
        /// <summary>
        /// Default country of Origin. Foreign key to Country table
        /// </summary>
        public Guid? MadeIn { get; set; }
        /// <summary>
        /// Product classification (Clasificación Normalizada de Articulos de Puericultura). Foreign key to Cnap table
        /// </summary>
        public Guid? CnapGuid { get; set; }
        /// <summary>
        /// Default Customs code for products in this category
        /// </summary>
        public string? CodiMercancia { get; set; }
        /// <summary>
        /// True if new products from this category should inherit tha category excerpt and description
        /// </summary>
        public bool DscPropagateToChildren { get; set; }
        /// <summary>
        /// True if the category should be displayed to consumers (on web site...)
        /// </summary>
        public bool WebEnabledConsumer { get; set; }
        /// <summary>
        /// True if the category should be visible to professionals
        /// </summary>
        public bool WebEnabledPro { get; set; }
        /// <summary>
        /// True if products under this category are just an administrative group of other products
        /// </summary>
        public bool IsBundle { get; set; }
        /// <summary>
        /// True if inventory not applicable (services...)
        /// </summary>
        public bool NoStk { get; set; }
        /// <summary>
        /// True when physical dimensions are not appliable to this category products (services...)
        /// </summary>
        public bool NoDimensions { get; set; }
        /// <summary>
        /// Default net weight, if any, in Kg
        /// </summary>
        public decimal KgNet { get; set; }
        /// <summary>
        /// Default gross weight, if any, in Kg (includes packaging)
        /// </summary>
        public decimal Kg { get; set; }
        /// <summary>
        /// Default volume of this category products, if any, in cubic meters, packaging included
        /// </summary>
        public decimal M3 { get; set; }
        /// <summary>
        /// Default length, if any, in mm (packaging included)
        /// </summary>
        public double? DimensionL { get; set; }
        /// <summary>
        /// Default width, if any, in mm (packaging included)
        /// </summary>
        public double? DimensionW { get; set; }
        /// <summary>
        /// Default height, if any, in mm (packaging included)
        /// </summary>
        public double? DimensionH { get; set; }
        /// <summary>
        /// Quantity of items per package
        /// </summary>
        public int InnerPack { get; set; }
        /// <summary>
        /// Quantity of items per master package
        /// </summary>
        public int OuterPack { get; set; }
        /// <summary>
        /// If true, orders are required to be multiple of InnerPack field value
        /// </summary>
        public bool ForzarInnerPack { get; set; }
        /// <summary>
        /// Category image (410x410 pixels)
        /// </summary>
        public byte[]? Image { get; set; }
        /// <summary>
        /// Thumbnail to ilustrate the category (150px)
        /// </summary>
        public byte[]? Thumbnail { get; set; }
        /// <summary>
        /// Year when this category products were or are expected to be launched to the market
        /// </summary>
        public short? LaunchYear { get; set; }
        /// <summary>
        /// Month when this category was launched to the market
        /// </summary>
        public short? LaunchMonth { get; set; }
        /// <summary>
        /// If not null, the category will remain hidden until disclosure date
        /// </summary>
        public DateTime? HideUntil { get; set; }
        /// <summary>
        /// True if the category is no longer active
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Date and time when this category was registered
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Last date and time when this category was edited
        /// </summary>
        public DateTime FchLastEdited { get; set; }

        public virtual Tpa BrandNavigation { get; set; } = null!;
        public virtual Cnap? CnapGu { get; set; }
        public virtual CodisMercancium? CodiMercanciaNavigation { get; set; }
        public virtual Country? MadeInNavigation { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<PremiumProduct> PremiumProducts { get; set; }
        public virtual ICollection<Web> Webs { get; set; }
    }
}
