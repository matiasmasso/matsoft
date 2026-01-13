using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;

namespace Shop4moms.Services
{
    public class ProductPluginService
    {
        public static string? ExpandPlugins(string? src, LangDTO lang, DTO.Integracions.Shop4moms.Cache cache)
        {
            string? retval = src;
            if (src != null)
            {
                var guids = ProductPluginModel.ExtractPluginGuids(src);
                foreach (var guid in guids)
                {
                    var plugin = cache.ProductPlugins.FirstOrDefault(x => x.Guid == guid);
                    if (plugin == null)
                        plugin = ProductPluginModel.CategoryCollection(cache, guid);
                    if (plugin != null)
                        retval = Regex.Replace(retval, plugin.Snippet(), Expansion(plugin, cache, lang), RegexOptions.IgnoreCase);
                }
                retval = Regex.Replace(retval!, "<a href = '#StoreLocator'></a>", ProductPluginModel.StoreLocatorCallToActionExpanded(lang));
            }
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

                var href = cache.Routes.LangText(product.Guid)?.Tradueix(lang) ?? "#";

                var caption = item.Caption.Tradueix(lang) ?? "";
                sb.AppendLine("<a class='ProductPluginItem' href='/" + href + "' title='" + caption + "'>");
                sb.AppendLine(" <div>");
                sb.AppendLine("     <img alt='"+caption+"' src='" + thumbnailUrl + "' width='" + width + "' height='" + height + "' alt='" + caption + "'/>");
                sb.AppendLine(" </div>");
                sb.AppendLine(" <div>" + caption + "</div>");
                sb.AppendLine(" <div class='BuyIt'>" + string.Format("{0} {1:N2} €", lang.Tradueix("Comprar per", "Comprar per", "Buy it for"),  cache.RetailPrices.FirstOrDefault(x=>x.Guid==item.Product)?.Value) + "</div>");
                sb.AppendLine("</a>");
            }
            var retval = sb.ToString();
            return retval;
        }

    }
}
