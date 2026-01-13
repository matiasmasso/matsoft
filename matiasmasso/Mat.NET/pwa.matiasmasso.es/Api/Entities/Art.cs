using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product sku properties
    /// </summary>
    public partial class Art
    {
        public Art()
        {
            ArtCustRefs = new HashSet<ArtCustRef>();
            ArtSpares = new HashSet<ArtSpare>();
            AuditStocks = new HashSet<AuditStock>();
            EdiversaDesadvItems = new HashSet<EdiversaDesadvItem>();
            EdiversaOrderItems = new HashSet<EdiversaOrderItem>();
            EdiversaSalesReportItems = new HashSet<EdiversaSalesReportItem>();
            Forecasts = new HashSet<Forecast>();
            ImportPrevisios = new HashSet<ImportPrevisio>();
            InverseSubstituteNavigation = new HashSet<Art>();
            Pncs = new HashSet<Pnc>();
            PriceListItemCustomers = new HashSet<PriceListItemCustomer>();
            ShoppingBasketItems = new HashSet<ShoppingBasketItem>();
            SkuBundleBundleNavigations = new HashSet<SkuBundle>();
            SkuBundleSkuNavigations = new HashSet<SkuBundle>();
            SkuMoqLocks = new HashSet<SkuMoqLock>();
            SkuWithChildNavigations = new HashSet<SkuWith>();
            SkuWithParentNavigations = new HashSet<SkuWith>();
            Sorteos = new HashSet<Sorteo>();
            Webs = new HashSet<Web>();
            WtbolBasketItems = new HashSet<WtbolBasketItem>();
            WtbolStocks = new HashSet<WtbolStock>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Category to which this Sku belongs. Foreign key to Stp table.
        /// </summary>
        public Guid Category { get; set; }
        public int Emp { get; set; }
        /// <summary>
        /// Unique product number within the Company
        /// </summary>
        public int Art1 { get; set; }
        public int Ord { get; set; }
        /// <summary>
        /// Manufacturer product code
        /// </summary>
        public string Ref { get; set; } = null!;
        /// <summary>
        /// Manufacturer product name (without manufacturer code)
        /// </summary>
        public string RefPrv { get; set; } = null!;
        /// <summary>
        /// EAN 13 product bar code
        /// </summary>
        public string? Cbar { get; set; }
        /// <summary>
        /// External package bar code
        /// </summary>
        public string? PackageEan { get; set; }
        /// <summary>
        /// Product classification (CNAP: Clasificación Normalizada de Articulos de Puericultura). Foreign key for Cnap table
        /// </summary>
        public Guid? CnapGuid { get; set; }
        /// <summary>
        /// Country of Origin. Foreign key for Country table
        /// </summary>
        public Guid? MadeIn { get; set; }
        /// <summary>
        /// Customs code. Foreign key for CodisMercancia table
        /// </summary>
        public string? CodiMercancia { get; set; }
        /// <summary>
        /// product VAT type applicable in Spain (standard, reduced, super reduced...)
        /// </summary>
        public byte IvaCod { get; set; }
        /// <summary>
        /// If true it prevents this product from being displayed on the website
        /// </summary>
        public bool NoWeb { get; set; }
        /// <summary>
        /// True for inmaterial products which do not participate in inventory (services...)
        /// </summary>
        public bool NoStk { get; set; }
        /// <summary>
        /// If true it is hidden from customer pricelists
        /// </summary>
        public bool NoTarifa { get; set; }
        /// <summary>
        /// If true, the product inherits logistic data from its category
        /// </summary>
        public bool HeredaDimensions { get; set; }
        /// <summary>
        /// True if dimensions not applicable for this product
        /// </summary>
        public bool NoDimensions { get; set; }
        /// <summary>
        /// Net weight, in Kg, without packaging
        /// </summary>
        public decimal KgNet { get; set; }
        /// <summary>
        /// Gross weight, in Kg, including packaging
        /// </summary>
        public decimal Kg { get; set; }
        /// <summary>
        /// Volume, in m3, including packaging
        /// </summary>
        public decimal M3 { get; set; }
        /// <summary>
        /// Product length in mm, packaging included
        /// </summary>
        public double DimensionL { get; set; }
        /// <summary>
        /// Product width in mm, packaging included
        /// </summary>
        public double DimensionW { get; set; }
        /// <summary>
        /// Product height in mm, packaging included
        /// </summary>
        public double DimensionH { get; set; }
        /// <summary>
        /// Units on each package
        /// </summary>
        public short InnerPack { get; set; }
        /// <summary>
        /// Units on a master package
        /// </summary>
        public short OuterPack { get; set; }
        /// <summary>
        /// If true, order quantities are restricted to multiple of InnerPack field value
        /// </summary>
        public bool ForzarInnerPack { get; set; }
        /// <summary>
        /// If  true, no new orders will be allowed further current stock + pending from supplier
        /// </summary>
        public bool LastProduction { get; set; }
        /// <summary>
        /// Average cost (Precio Medio de Compra) for current stock. Used in inventory and updated daily by a Windows service
        /// </summary>
        public decimal Pmc { get; set; }
        /// <summary>
        /// If true the product inherits descriptions from its category
        /// </summary>
        public byte Hereda { get; set; }
        /// <summary>
        /// Product image, 700x800 pixels or whatever defined on DTOProductSku.IMAGEWIDTH and DTOProductSku.IMAGEHEIGHT classes
        /// </summary>
        public byte[]? Image { get; set; }
        /// <summary>
        /// True if an image has been uploaded for this product
        /// </summary>
        public bool ImgExists { get; set; }
        /// <summary>
        /// Date and time for last image upload
        /// </summary>
        public DateTime ImgFch { get; set; }
        /// <summary>
        /// Product thumbnail image, 150 pixels width or whatever defined on DTOProductSku class constants
        /// </summary>
        public byte[]? Thumbnail { get; set; }
        /// <summary>
        /// True if the product is offered at the website outlet
        /// </summary>
        public bool Outlet { get; set; }
        /// <summary>
        /// Discount offered at website outlet for this product
        /// </summary>
        public int OutletDto { get; set; }
        /// <summary>
        /// Minimum quantity required to get the discount offered at website outlet
        /// </summary>
        public int OutletQty { get; set; }
        /// <summary>
        /// If true, it won&apos;t be displayed to customers or reps
        /// </summary>
        public bool NoPro { get; set; }
        /// <summary>
        /// True if the product is an agregation of different products
        /// </summary>
        public bool IsBundle { get; set; }
        /// <summary>
        /// Discount applicable when purchased as a bundle over the total of the amount
        /// </summary>
        public decimal? BundleDto { get; set; }
        /// <summary>
        /// Replacement product for outdated skus. Foreign key for same table
        /// </summary>
        public Guid? Substitute { get; set; }
        /// <summary>
        /// Minimum stock to keep for this product
        /// </summary>
        public int? SecurityStock { get; set; }
        /// <summary>
        /// Expected availability date, if it is not currently available
        /// </summary>
        public DateTime? Availability { get; set; }
        /// <summary>
        /// The product won&apos;t be displayed until this date if not null
        /// </summary>
        public DateTime? HideUntil { get; set; }
        /// <summary>
        /// True for outdated products
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Date and time the product was outdated
        /// </summary>
        public DateTime? FchObsoleto { get; set; }
        public DateTime? ObsoletoConfirmed { get; set; }
        /// <summary>
        /// Date and time the product was registered
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Last time the product was edited
        /// </summary>
        public DateTime FchLastEdited { get; set; }

        public virtual Stp CategoryNavigation { get; set; } = null!;
        public virtual Cnap? CnapGu { get; set; }
        public virtual CodisMercancium? CodiMercanciaNavigation { get; set; }
        public virtual Country? MadeInNavigation { get; set; }
        public virtual Art? SubstituteNavigation { get; set; }
        public virtual ICollection<ArtCustRef> ArtCustRefs { get; set; }
        public virtual ICollection<ArtSpare> ArtSpares { get; set; }
        public virtual ICollection<AuditStock> AuditStocks { get; set; }
        public virtual ICollection<EdiversaDesadvItem> EdiversaDesadvItems { get; set; }
        public virtual ICollection<EdiversaOrderItem> EdiversaOrderItems { get; set; }
        public virtual ICollection<EdiversaSalesReportItem> EdiversaSalesReportItems { get; set; }
        public virtual ICollection<Forecast> Forecasts { get; set; }
        public virtual ICollection<ImportPrevisio> ImportPrevisios { get; set; }
        public virtual ICollection<Art> InverseSubstituteNavigation { get; set; }
        public virtual ICollection<Pnc> Pncs { get; set; }
        public virtual ICollection<PriceListItemCustomer> PriceListItemCustomers { get; set; }
        public virtual ICollection<ShoppingBasketItem> ShoppingBasketItems { get; set; }
        public virtual ICollection<SkuBundle> SkuBundleBundleNavigations { get; set; }
        public virtual ICollection<SkuBundle> SkuBundleSkuNavigations { get; set; }
        public virtual ICollection<SkuMoqLock> SkuMoqLocks { get; set; }
        public virtual ICollection<SkuWith> SkuWithChildNavigations { get; set; }
        public virtual ICollection<SkuWith> SkuWithParentNavigations { get; set; }
        public virtual ICollection<Sorteo> Sorteos { get; set; }
        public virtual ICollection<Web> Webs { get; set; }
        public virtual ICollection<WtbolBasketItem> WtbolBasketItems { get; set; }
        public virtual ICollection<WtbolStock> WtbolStocks { get; set; }
    }
}
