using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using Web.Services;

namespace Web.Services
{
    public class ProductPluginsService
    {
        public List<ProductPluginModel>? Values { get; set; }

        private AppStateService appstate;
        private ProductCategoriesService categoriesService;
        private ProductSkusService skusService;
        private CanonicalUrlsService canonicalUrlsService;
        private RetailPricesService retailPricesService;

        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        public ProductPluginsService(
            AppStateService appstate,
            ProductCategoriesService categories,
            ProductSkusService skus,
            CanonicalUrlsService canonicalUrls,
            RetailPricesService retailPrices
            )
        {
            this.appstate = appstate;
            this.categoriesService = categories;
            this.skusService = skus;
            this.canonicalUrlsService = canonicalUrls;
            this.retailPricesService = retailPrices;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                Values = await appstate.GetAsync<List<ProductPluginModel>>("ProductPlugins") ?? new();
                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public ProductPluginModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);



        #region misc

        //public string? ExpandPlugins(string? src,
        //LangDTO lang,
        //ProductCategoryModel? category,
        //List<GuidDecimal>? retailPrices)

        //{
        //    string? retval = src;
        //    if (src != null)
        //    {
        //        var guids = ProductPluginModel.ExtractPluginGuids(src);

        //        var products = new List<ProductModel>();
        //        products.AddRange(categoriesService.Values ?? new());
        //        products.AddRange(skusService.Values ?? new());

        //        foreach (var guid in guids)
        //        {
        //            var plugin = Values?.FirstOrDefault(x => x.Guid == guid);
        //            if (plugin == null)
        //            {
        //                var categorySkus = products?.Where(x => x.Src == ProductModel.SourceCods.Sku && ((ProductSkuModel)x).Category == category.Guid).
        //                    Select(x => (ProductSkuModel)x).ToList() ?? new();
        //                plugin = ProductPluginModel.CategoryCollection(category, categorySkus);
        //            }
        //            if (plugin != null)
        //                retval = Regex.Replace(retval, plugin.Snippet(), Expansion(plugin, lang), RegexOptions.IgnoreCase);
        //        }
        //        //retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", ProductPluginModel.StoreLocatorCallToActionExpanded(lang));
        //        retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", "");
        //    }
        //    return retval;
        //}
        public string? ExpandPlugins(string? src, LangDTO lang)
        {
            string? retval = src;
            if (src != null)
            {
                var guids = ProductPluginModel.ExtractPluginGuids(src);
                foreach (var guid in guids)
                {
                    var category = new ProductCategoryModel(guid);
                    var plugin = Values?.FirstOrDefault(x => x.Guid == guid);
                    if (plugin == null)
                        plugin = ProductPluginModel.CategoryCollection(category, skusService.Values);
                    if (plugin != null)
                        retval = Regex.Replace(retval, plugin.Snippet(), Expansion(plugin, lang), RegexOptions.IgnoreCase);
                }
                //retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", ProductPluginModel.StoreLocatorCallToActionExpanded(lang));
                retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", "");
            }
            return retval;
        }


        public string Expansion(ProductPluginModel plugin, LangDTO lang)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<!------------------------------------- -->");
            sb.AppendLine("<div class='PluginWrapper'>");
            sb.AppendLine(" <div class='Plugin' data-pluginId='" + plugin.Guid.ToString() + "'>");
            sb.AppendLine("     <a href='#' class='ChevronLeft'>");
            sb.AppendLine("         <svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\">");
            sb.AppendLine("             <path d=\"M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l192 192c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L77.3 256 246.6 86.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-192 192z\"/>");
            sb.AppendLine("         </svg>");
            sb.AppendLine("     </a>");
            sb.AppendLine("     <div>");

            foreach (var item in plugin.Items)
            {
                sb.AppendLine(ItemExpansion(item, lang));
            }

            sb.AppendLine("     </div>");
            sb.AppendLine("     <a href='#' class='ChevronRight'>");
            sb.AppendLine("         <svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\">");
            sb.AppendLine("             <path d=\"M310.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-192 192c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L242.7 256 73.4 86.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l192 192z\"/>");
            sb.AppendLine("         </svg>");
            sb.AppendLine("     </a>");
            sb.AppendLine(" </div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<!------------------------------------- -->");
            var retval = sb.ToString();
            return retval;
        }
        public string ItemExpansion(ProductPluginModel.Item item, LangDTO lang)
        {
            string retval = "";
            ProductModel? product = categoriesService.GetValue(item.Product);
            if (product == null) product = skusService.GetValue(item.Product);
            if (product != null && product!.Obsoleto == false)
            {

                var thumbnailUrl = product.ThumbnailUrl();
                var width = ProductSkuModel.THUMBNAIL_WIDTH;
                var height = ProductSkuModel.THUMBNAIL_HEIGHT;
                if (product.Src == ProductModel.SourceCods.Sku) thumbnailUrl = ProductSkuModel.ThumbnailUrl(product.Guid);
                else if (product.Src == ProductModel.SourceCods.Category)
                {
                    thumbnailUrl = ProductCategoryModel.ImageUrl(product.Guid);
                    width = ProductCategoryModel.THUMBNAIL_WIDTH;
                    height = ProductCategoryModel.THUMBNAIL_HEIGHT;
                }

                var href = canonicalUrlsService.GetValue(product.Guid)?.Url.Tradueix(lang) ?? "#";
                //culture.relativeUrl?

                var caption = item.Caption.Tradueix(lang) ?? "";
                var alt = caption;
                var text = caption;
                retval = String.Format("<a href='{0}' title='{1}'><div><img src='{2}' width='{3}' height='{4}' alt='{1}'/></div><div>{5}</div></a>", href, alt, thumbnailUrl, width, height, text);

             }
            return retval;
        }

        public string ItemExpansionBuyIt(ProductPluginModel.Item item, LangDTO lang)
        {
            var sb = new System.Text.StringBuilder();
            ProductModel? product = categoriesService.GetValue(item.Product);
            if (product == null) product = skusService.GetValue(item.Product);
            if (product != null && product!.Obsoleto == false)
            {
                var thumbnailUrl = product.ThumbnailUrl();
                var width = ProductSkuModel.THUMBNAIL_WIDTH;
                var height = ProductSkuModel.THUMBNAIL_HEIGHT;
                if (product.Src == ProductModel.SourceCods.Sku) thumbnailUrl = ProductSkuModel.ThumbnailUrl(product.Guid);
                else if (product.Src == ProductModel.SourceCods.Category)
                {
                    thumbnailUrl = ProductCategoryModel.ImageUrl(product.Guid);
                    width = ProductCategoryModel.THUMBNAIL_WIDTH;
                    height = ProductCategoryModel.THUMBNAIL_HEIGHT;
                }

                var href = canonicalUrlsService.GetValue(product.Guid)?.Url.Tradueix(lang) ?? "#";
                //culture.relativeUrl?

                var caption = item.Caption.Tradueix(lang) ?? "";
                sb.AppendLine("<a class='ProductPluginItem' href='/" + href + "' title='" + caption + "'>");
                sb.AppendLine(" <div>");
                sb.AppendLine("     <img alt='" + caption + "' src='" + thumbnailUrl + "' width='" + width + "' height='" + height + "' alt='" + caption + "'/>");
                sb.AppendLine(" </div>");
                sb.AppendLine(" <div>" + caption + "</div>");
                sb.AppendLine(" <div class='BuyIt'>" + string.Format("{0} {1:N2} €", lang.Tradueix("Comprar por", "Comprar per", "Buy it for"), retailPricesService.GetValue(item.Product)?.Value) + "</div>");
                sb.AppendLine("</a>");
            }
            var retval = sb.ToString();
            return retval;
        }

        #endregion

    }
}
