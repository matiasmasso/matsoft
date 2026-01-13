using DocumentFormat.OpenXml.Drawing;
using DTO;
using System.Text.RegularExpressions;

namespace Web.Services
{
    public class ProductsService : IDisposable
    {

        public event Action<Exception?>? OnChange;

        private AppStateService appstate;
        private ProductBrandsService brandsService;
        private ProductDeptsService deptsService;
        private ProductCategoriesService categoriesService;
        private ProductSkusService skusService;
        private ProductSkuStocksService stocksService;
        private ProductSkuPncsService pncsService;
        private RetailPricesService retailPricesService;
        private PriceListSuppliersService priceListSuppliersService;
        private LangTextsService langtextsService;
        private CanonicalUrlsService canonicalUrlsService;
        private ProductPluginsService productpluginsService;
        private CustomerProductsService customerProductsService;
        private ChannelDtosOnRrppService channelDtosOnRrppService;


        public ProductsService(AppStateService appstate,
            ProductBrandsService brands,
            ProductDeptsService depts,
            ProductCategoriesService categories,
            ProductSkusService skus,
            ProductSkuStocksService stocks,
            ProductSkuPncsService pncs,
            RetailPricesService rrpps,
            PriceListSuppliersService priceListSuppliersService,
            LangTextsService langtexts,
            CanonicalUrlsService canonicalUrls,
            ProductPluginsService productplugins,
            CustomerProductsService customerProductsService,
            ChannelDtosOnRrppService channelDtosOnRrppService)
        {
            this.appstate = appstate;
            this.brandsService = brands;
            this.deptsService = depts;
            this.categoriesService = categories;
            this.skusService = skus;
            this.stocksService = stocks;
            this.pncsService = pncs;
            this.retailPricesService = rrpps;
            this.priceListSuppliersService = priceListSuppliersService;
            this.langtextsService = langtexts;
            this.canonicalUrlsService = canonicalUrls;
            this.productpluginsService = productplugins;
            this.customerProductsService = customerProductsService;
            this.channelDtosOnRrppService = channelDtosOnRrppService;

            brandsService.OnChange += NotifyChange;
            deptsService.OnChange += NotifyChange;
            categoriesService.OnChange += NotifyChange;
            skusService.OnChange += NotifyChange;
            langtextsService.OnChange += NotifyChange;
            canonicalUrlsService.OnChange += NotifyChange;
            customerProductsService.OnChange += NotifyChange;
            channelDtosOnRrppService.OnChange += NotifyChange;
            retailPricesService.OnChange += NotifyChange;
            priceListSuppliersService.OnChange += NotifyChange;
        }

        public void SetDirtyIfNeeded(List<DirtyTableModel> items)
        {

        }

        private void ChannelDtosOnRrppService_OnChange(Exception? obj)
        {
            throw new NotImplementedException();
        }



        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public DbState State()
        {
            if (brandsService.State == DbState.IsLoading
                || deptsService.State == DbState.IsLoading
                || categoriesService.State == DbState.IsLoading
                || skusService.State == DbState.IsLoading
                || langtextsService.State == DbState.IsLoading
                || canonicalUrlsService.State == DbState.IsLoading
                || customerProductsService.State == DbState.IsLoading
                || channelDtosOnRrppService.State == DbState.IsLoading)
                return DbState.IsLoading;
            else
                return DbState.StandBy;
        }


        public string? LandingPage(ProductModel? item, CultureService culture)
        {
            string? retval = null;
            if (item != null)
            {
                var absolutePath = canonicalUrlsService.GetValue(item.Guid)?.Url?.Tradueix(culture.Lang());
                retval = culture.RelativeUrl(absolutePath);
                //if (item.Src == ProductModel.SourceCods.Brand)
                //{
                //    ProductBrandModel brand = (ProductBrandModel)item;
                //    var brandSegment = langtextsService.Segment(brand.Guid, culture.Lang());
                //    retval = culture.RelativeUrl(brandSegment);
                //}
                //else if (item.Src == ProductModel.SourceCods.Dept)
                //{
                //    ProductBrandModel? brand = brandsService.Values?.FirstOrDefault(x => x.Guid == item.Guid);
                //    ProductDeptModel? dept = deptsService.Find(item.Guid);
                //    var brandSegment = langtextsService.Segment(dept?.Brand, culture.Lang());
                //    var deptSegment = langtextsService.Segment(dept?.Guid, culture.Lang());
                //    retval = culture.RelativeUrl(brandSegment, deptSegment);
                //}
                //else if (item.Src == ProductModel.SourceCods.Category)
                //{
                //    ProductCategoryModel category = (ProductCategoryModel)item;
                //    ProductBrandModel? brand = brandsService.Values?.FirstOrDefault(x => x.Guid == category.Brand);
                //    var brandSegment = langtextsService.Segment(category?.Brand, culture.Lang());
                //    var categorySegment = langtextsService.Segment(category?.Guid, culture.Lang());
                //    if (brand?.IncludeDeptOnUrl ?? false)
                //    {
                //        var deptSegment = langtextsService.Segment(category?.Dept, culture.Lang());
                //        retval = culture.RelativeUrl(brandSegment, deptSegment, categorySegment);
                //    }
                //    else
                //        retval = culture.RelativeUrl(brandSegment, categorySegment);
                //}
            }
            return retval;
        }

        public ProductModel? ProductFromLandingPage(string landingPage, LangDTO lang)
        {
            ProductModel? retval = null;
            var canonicalUrl = canonicalUrlsService.GetValue(landingPage);
            if (canonicalUrl != null)
            {
                retval = GetValue(canonicalUrl.Target);
            }
            //var searchTerm = landingPage.ToLower().Trim();
            //var segments = searchTerm.Split('/');
            //var src = LangTextModel.Srcs.ProductUrl;
            //if (segments.Any())
            //{
            //    var brandLangSegment = langtextsService.GetValuesFromContent(src, lang, segments.First())?.FirstOrDefault();
            //    var brand = brandLangSegment == null ? null : brandsService.GetValue((Guid)brandLangSegment.Guid!);
            //    if (segments.Length == 1)
            //        retval = brand;
            //    else
            //    {
            //        var secondSegments = langtextsService.GetValuesFromContent(src, lang, segments[1]);
            //        if (brand?.IncludeDeptOnUrl ?? false)
            //        {
            //            var depts = deptsService.Values?.Where(x => x.Brand == brand?.Guid).ToList();
            //            var dept = depts?.FirstOrDefault(x => secondSegments?.Any(y => x.Guid == y.Guid) ?? false);
            //            if (segments.Length == 2) retval = dept;
            //        }
            //        else
            //        {
            //            var categories = categoriesService.Values?.Where(x => x.Brand == brand?.Guid).ToList();
            //            var category = categories?.FirstOrDefault(x => secondSegments?.Any(y => x.Guid == y.Guid) ?? false);
            //            if (segments.Length == 2) retval = category;
            //        }
            //        if (segments.Length > 2)
            //        {

            //        }

            //    }

            //}
            return retval;
        }

        public ProductModel? GetValue(Guid? guid)
        {
            ProductModel? retval = null;
            if (guid != null)
            {
                retval = brandsService.GetValue(guid);
                if (retval == null) retval = deptsService.GetValue(guid);
                if (retval == null) retval = categoriesService.GetValue(guid);
                if (retval == null) retval = skusService.GetValue(guid);
            }
            return retval;
        }

        public LangTextModel? Nom(Guid? guid)
        {
            LangTextModel? retval = null;
            ProductModel? product = null;
            if (guid != null)
            {
                product = brandsService.GetValue(guid);
                if (product == null) product = deptsService.GetValue(guid);
                if (product == null) product = categoriesService.GetValue(guid);
                if (product == null)
                {
                    var sku = skusService.GetValue(guid);
                    retval = sku?.NomLlarg;
                } else
                {
                    retval = product.Nom;
                }
            }
            return retval;
        }
        public ProductBrandModel? Brand(Guid? guid)
        {
            ProductBrandModel? retval = brandsService.GetValue(guid);
            if (retval == null)
            {
                var dept = deptsService.GetValue(guid);
                if (dept != null) retval = brandsService.GetValue(dept.Brand);
            }
            if (retval == null)
            {
                var category = categoriesService.GetValue(guid);
                if (category != null) retval = brandsService.GetValue(category.Brand);
            }
            if (retval == null)
            {
                var sku = skusService.GetValue(guid);
                if (sku != null)
                {
                    var category = categoriesService.GetValue(sku.Category);
                    if (category != null) retval = brandsService.GetValue(category.Brand);
                }
            }
            return retval;
        }



        public ProductBrandModel? Brand(ProductDeptModel? dept) => Brand(dept?.Brand);
        public ProductBrandModel? Brand(ProductCategoryModel? category) => Brand(category?.Brand);

        public List<ProductBrandModel>? Brands(bool onlyActive = true) => brandsService.Values?.Where(x => !x.Obsoleto || !onlyActive).ToList();
        public List<ProductDeptModel>? Depts(ProductBrandModel? brand) => deptsService.BrandDepts(brand);
        public ProductDeptModel? Dept(Guid? guid) => deptsService.GetValue(guid);
        public ProductDeptModel? Dept(ProductCategoryModel? category) => Dept(category?.Dept);

        public ProductCategoryModel? Category(Guid? guid)
        {
            ProductCategoryModel? retval = categoriesService.GetValue(guid);
            if (retval == null)
            {
                var sku = skusService.GetValue(guid);
                if (sku != null)
                    retval = categoriesService.GetValue(sku.Category);
            }
            return retval;
        }

        public ProductSkuModel? SkuFromEan(string? ean) => skusService.FromEan(ean);
        public ProductSkuModel? SkuFromId(int? id) => skusService.FromId(id);
        public List<ProductSkuModel>? Skus(bool onlyActive = true) => skusService.GetValues(onlyActive);
        public List<ProductSkuModel>? Skus(ProductCategoryModel? category, bool onlyActive) => skusService.GetValues(category, onlyActive);
        public List<ProductSkuModel>? Skus(ProductBrandModel? brand, bool onlyActive) {
            var categories = Categories(brand, onlyActive);
            var retval = skusService.GetValues(categories);
            return retval;
        }

        public List<ProductCategoryModel>? Categories(ProductBrandModel? brand, bool onlyActive) => categoriesService.GetValues(brand, onlyActive);
        public List<ProductCategoryModel>? Categories(bool onlyActive = true) => categoriesService.GetValues(onlyActive);

        public List<ProductCategoryModel>? Categories(ProductDeptModel? dept, bool onlyActive) => categoriesService.GetValues(dept, onlyActive);
        public ProductSkuModel? Sku(Guid? guid) => skusService.GetValue(guid);
        public SkuStockModel? Stock(Guid? guid, Guid? mgz) => stocksService.GetValue(guid, mgz);
        public int AvailableStock(Guid? sku)
        {
            MgzModel mgz = MgzModel.Default()!;
            var stock = stocksService.GetValue(sku, mgz.Guid);
            var pnc = pncsService.GetValue(sku);
            var retval = stock?.Available(pnc) ?? 0;
            return retval;
        }
        public SkuPncModel? Pnc(Guid? guid) => pncsService.GetValue(guid);
        public Decimal? RetailPrice(Guid? guid) => retailPricesService.GetValue(guid)?.Value;
        public Decimal? SkuCost(Guid? guid) => priceListSuppliersService.GetValue(guid);

        public string? SkuThumbnailUrl(Guid? guid) => skusService.GetValue(guid)?.ThumbnailUrl();

        public List<ProductSkuModel>? Skus(string? searchterm) {
            if (string.IsNullOrEmpty(searchterm))
                return skusService.Values;
            else
                return skusService.Values?.Where(x => x.Matches(searchterm)).ToList();
        }

        public Guid? MadeInOrInherited(ProductSkuModel? sku) {
            Guid? retval = sku?.MadeIn;
            if(retval == null)
            {
                var category = Category(sku?.Category);
                retval = category?.MadeIn;
                if (retval == null)
                    retval = Brand(category?.Brand)?.MadeIn;
            }
            return retval;
        }
        public string? CustomerSkuRef(Guid? customer, Guid? sku) => customerProductsService.GetValue(customer, sku)?.Ref;

        public int Moq(ProductSkuModel? sku)
        {
            int innerPack = 0;
            bool forzarInnerPack = false;
            if (sku != null)
            {
                if (sku.HeredaDimensions)
                {
                    var category = Category(sku.Category);
                    innerPack = category?.InnerPack ?? 0;
                    forzarInnerPack = category?.ForzarInnerPack ?? false;
                }
                else
                {
                    innerPack = sku?.InnerPack ?? 0;
                    forzarInnerPack = sku?.ForzarInnerPack ?? false;
                }
            }
            int retval = forzarInnerPack ? innerPack : 1;
            return retval;
        }
        public List<Box> Breadcrumbs(ProductModel model, CultureService culture)
        {
            var retval = new List<Box>();
            if (model != null)
            {
                if (model.Src == ProductModel.SourceCods.Brand)
                {
                    retval.Add(Box(model, culture));
                }
                else if (model.Src == ProductModel.SourceCods.Dept)
                {
                    var dept = (ProductDeptModel)model;
                    retval.Add(Box(Brand(dept), culture));
                    retval.Add(Box(model, culture));
                }
                else if (model.Src == ProductModel.SourceCods.Category)
                {
                    var category = (ProductCategoryModel)model;
                    retval.Add(Box(Brand(category), culture));
                    if (category.Dept != null)
                        retval.Add(Box(Dept(category), culture));
                    retval.Add(Box(category, culture));
                }
                else if (model.Src == ProductModel.SourceCods.Sku)
                {
                    //var sku = (ProductSkuModel)model;
                    //var category = cache.Category(sku.Category);
                    //retval.Add(Box(cache.Brand((Guid)category!.Brand!), cache, lang));
                    //if (category.Dept != null)
                    //    retval.Add(Box(cache.Dept((Guid)category.Dept), cache, lang));
                    //retval.Add(Box(category, cache, lang));
                    //retval.Add(Box(model, cache, lang));
                }
            }
            return retval;
        }
        private Box Box(ProductModel? product, CultureService culture)
        {
            return new Box
            {
                Caption = product?.Nom?.Tradueix(culture.Lang()) ?? "?",
                Url = LandingPage(product, culture)
            };
        }

        public string Content(ProductModel? product, LangDTO lang)
        {
            string? retval = null;
            if (product != null)
            {
                var langtextContent = langtextsService.GetValue(product.Guid, LangTextModel.Srcs.ProductText);
                var html = langtextContent?.Tradueix(lang)?.Html();
                retval = productpluginsService.ExpandPlugins(html, lang);
            }
            return retval ?? "";
        }


        public string ContentBeforeMoreOrDefault(ProductModel? product, LangDTO lang)
        {
            var retval = string.Empty;
            var content = Content(product, lang);
            if (!string.IsNullOrEmpty(content))
            {
                var label = "<more>";
                var pos = content.IndexOf(label);
                retval = pos > 0 ? content.Substring(0, pos) : content;
            }
            return retval;
        }

        public string ContentAfterMoreOrDefault(ProductModel? product, LangDTO lang)
        {
            var retval = string.Empty;
            var content = Content(product, lang);
            if (!string.IsNullOrEmpty(content))
            {
                var label = "<more>";
                var pos = content.IndexOf(label);
                retval = pos > 0 ? content.Substring(pos + label.Length) : string.Empty;
            }

            return retval;
        }

        public int InnerPackOrInherited(Guid? skuGuid)
        {
            var sku =skusService.GetValue(skuGuid);
            var retval = sku?.InnerPack;
            if ((sku?.HeredaDimensions ?? false) && sku?.Category != null)
            {
                var category = categoriesService.GetValue(sku.Category);
                retval = category?.InnerPack;
            }
            if (retval < 1)
                retval = 1;
            return retval ?? 1;
        }

        public ImplicitDiscountModel? DiscountOnChannel(Guid? channel, ProductSkuModel sku, DateTime? fch=null)
        {
            var values = channelDtosOnRrppService.ActiveValues(channel, fch);
            var retval = values?.FirstOrDefault(x => x.Product == sku.Guid);
            if(retval == null)
            {
                retval = values?.FirstOrDefault(x => x.Product == sku.Category);
                if (retval == null)
                {
                    var brand = categoriesService.GetValue(sku.Category)?.Brand;
                    retval = values?.FirstOrDefault(x => x.Product == brand);
                }
            }
            return retval;
        }

        public string? ThumbnailUrl(Guid? guid) => guid == null ? string.Empty : Globals.ApiUrl("product/thumbnail", guid.ToString()+".jpg");
        public void Dispose()
        {
            brandsService.OnChange -= NotifyChange;
            deptsService.OnChange -= NotifyChange;
            categoriesService.OnChange -= NotifyChange;
            skusService.OnChange -= NotifyChange;
            langtextsService.OnChange -= NotifyChange;
            canonicalUrlsService.OnChange -= NotifyChange;
            customerProductsService.OnChange -= NotifyChange;
            channelDtosOnRrppService.OnChange -= NotifyChange;
            retailPricesService.OnChange -= NotifyChange;
            priceListSuppliersService.OnChange -= NotifyChange;
        }

        public async Task UpdateAsync(ProductModel? product)
        {
            if (product is ProductSkuModel) { 
                await skusService.UpdateAsync((ProductSkuModel)product);
            } else 
                throw new NotImplementedException();
        }

    }
}
