using System.Text.RegularExpressions;
using Shop4moms.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using Microsoft.AspNetCore.Components;

namespace Shop4moms.Services
{
    public class ProductPluginService
    {
        public static string? ExpandPlugins(string? src,
            LangDTO lang,
            ProductCategoryModel? category,
            List<ProductPluginModel>? productPlugins,
            List<ProductModel>? products,
            RouteModel.Collection? routes,
            List<GuidDecimal>? retailPrices)

        {
            string? retval = src;
            if (src != null)
            {
                var guids = ProductPluginModel.ExtractPluginGuids(src);
                foreach (var guid in guids)
                {
                    var plugin = productPlugins?.FirstOrDefault(x => x.Guid == guid);
                    if (plugin == null)
                    {
                        var categorySkus = products?.Where(x => x.Src == ProductModel.SourceCods.Sku && ((ProductSkuModel)x).Category == category.Guid).
                            Select(x => (ProductSkuModel)x).ToList() ?? new();
                        plugin = ProductPluginModel.CategoryCollection(category, categorySkus);
                    }
                    if (plugin != null)
                        retval = Regex.Replace(retval, plugin.Snippet(), Expansion(plugin, lang, products, routes, retailPrices), RegexOptions.IgnoreCase);
                }
                //retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", ProductPluginModel.StoreLocatorCallToActionExpanded(lang));
                retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", "");
            }
            return retval;
        }


        public static string Expansion(ProductPluginModel plugin, 
            LangDTO lang,
            List<ProductModel> products,
            RouteModel.Collection routes,
            List<GuidDecimal> retailPrices)
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
                var product = products.FirstOrDefault(x => x.Guid == item.Product);
                if (product != null)
                {
                    sb.AppendLine(ItemExpansion(item, lang, product, routes, retailPrices));
                }
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


        public static string Expansion(ProductPluginModel plugin, DTO.Integracions.Shop4moms.Cache cache, LangDTO lang)
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
                sb.AppendLine(ItemExpansion(item, cache, lang));
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

        public static string ItemExpansion(ProductPluginModel.Item item,
            LangDTO lang,
            ProductModel product,
            RouteModel.Collection routes,
            List<GuidDecimal> retailPrices)
        {
            var sb = new System.Text.StringBuilder();
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

                var href = CanonicalUrl(routes, product, lang) ?? "#";

                var caption = item.Caption.Tradueix(lang) ?? "";
                sb.AppendLine("<a class='ProductPluginItem' href='/" + href + "' title='" + caption + "'>");
                sb.AppendLine(" <div>");
                sb.AppendLine("     <img alt='" + caption + "' src='" + thumbnailUrl + "' width='" + width + "' height='" + height + "' alt='" + caption + "'/>");
                sb.AppendLine(" </div>");
                sb.AppendLine(" <div>" + caption + "</div>");
                sb.AppendLine(" <div class='BuyIt'>" + string.Format("{0} {1:N2} €", lang.Tradueix("Comprar por", "Comprar per", "Buy it for"), retailPrices.FirstOrDefault(x => x.Guid == item.Product)?.Value) + "</div>");
                sb.AppendLine("</a>");
            }
            var retval = sb.ToString();
            return retval;
        }

        public static string ItemExpansion(ProductPluginModel.Item item, DTO.Integracions.Shop4moms.Cache cache, LangDTO lang)
        {
            var sb = new System.Text.StringBuilder();
            var product = cache.ProductFromGuid(item.Product);
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

                var href = CanonicalUrl(cache, product, lang) ?? "#";

                var caption = item.Caption.Tradueix(lang) ?? "";
                sb.AppendLine("<a class='ProductPluginItem' href='/" + href + "' title='" + caption + "'>");
                sb.AppendLine(" <div>");
                sb.AppendLine("     <img src='" + thumbnailUrl + "' width='" + width + "' height='" + height + "' alt='" + caption + "'/>");
                sb.AppendLine(" </div>");
                sb.AppendLine(" <div>" + caption + "</div>");
                sb.AppendLine(" <div class='BuyIt'>" + string.Format("{0} {1:N2} €", lang.Tradueix("Comprar por", "Comprar per", "Buy it for"), cache.RetailPrices.FirstOrDefault(x => x.Guid == item.Product)?.Value) + "</div>");
                sb.AppendLine("</a>");
            }
            var retval = sb.ToString();
            return retval;
        }

        public static string? CanonicalUrl(RouteModel.Collection routes, ProductModel? product, LangDTO lang)
        {
            var retval = product == null ? null : routes.LangText(product.Guid)?.Tradueix(lang);
            if (retval != null)
            {
                if (lang.IsCat() | lang.IsEng())
                    retval = string.Format("{0}/{1}", lang.Culture2Digits(), retval);
            }
            return retval;
        }
        private static string? CanonicalUrl(DTO.Integracions.Shop4moms.Cache cache, ProductModel? product, LangDTO lang)
        {
            var retval = product == null ? null : cache.Routes.LangText(product.Guid)?.Tradueix(lang);
            if (retval != null)
            {
                if (lang.IsCat() | lang.IsEng())
                    retval = string.Format("{0}/{1}", lang.Culture2Digits(), retval);
            }
            return retval;
        }

    }
}
