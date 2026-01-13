using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using static DTO.CatalogDTO;

namespace DTO
{
    public class ProductPluginModel : BaseGuid
    {
        public List<Item> Items { get; set; } = new();
        public ProductPluginModel() : base() { }
        public ProductPluginModel(Guid guid) : base(guid) { }

        public enum Modes
        {
            Custom,
            Collection,
            Accessories
        }

        public Item AddItem(Guid product, string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            var retval = new Item
            {
                Product = product,
                Caption = new LangTextModel(esp ?? "", cat, eng, por)
            };
            Items.Add(retval);
            return retval;
        }

        public static List<Guid> ExtractPluginGuids(string? src)
        {
            var retval = new List<Guid>();
            if (src != null)
            {
                string guidRegexPattern = @"[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}";
                string pattern = string.Format(@"<div data-ProductPlugin='{0}'.*?><\/div>", guidRegexPattern);

                foreach (Match m in Regex.Matches(src, pattern, RegexOptions.IgnoreCase))
                {
                    string plugin = m.Value;
                    var sGuid = Regex.Match(plugin, guidRegexPattern).Value;
                    retval.Add(new Guid(sGuid));
                }
            }
            return retval;
        }
        public string Snippet()
        {
            //var retval = Snippet(base.Guid.ToString(), Modes.Custom);
            var retval = string.Format(@"<div data-ProductPlugin=\'{0}\'.*?><\/div>", base.Guid.ToString());
            //string guidRegexPattern = @"[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}";
            //string retval = string.Format(@"<div data-ProductPlugin='{0}'.*?></div>", guidRegexPattern);
            return retval;
        }

        public static string Snippet(string id, Modes mode)
        {
            return string.Format("<div data-ProductPlugin='{0}' data-ProductPluginMode='{1}'></div>", id, mode.ToString());
        }

        public string Expansion(CacheDTO cache, LangDTO lang)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<!------------------------------------- -->");
            sb.AppendLine("<div class='PluginWrapper'>");
            sb.AppendLine(" <div class='Plugin' data-pluginId='" + Guid.ToString() + "'>");
            sb.AppendLine("     <a href='#' class='ChevronLeft'>");
            sb.AppendLine("         <svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\">");
            sb.AppendLine("             <path d=\"M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l192 192c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L77.3 256 246.6 86.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-192 192z\"/>");
            sb.AppendLine("         </svg>");
            sb.AppendLine("     </a>");
            sb.AppendLine("     <div>");

            foreach (var item in Items)
            {
                sb.AppendLine(item.Expansion( cache, lang));
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

        public static ProductPluginModel? CategoryCollection(ProductCategoryModel? category, List<ProductSkuModel>?skus)
        {
            ProductPluginModel? retval = null;
            if (category != null)
            {
                retval = new ProductPluginModel(category.Guid);
                retval.Items = skus?
                    .Where(x => x.Category == category.Guid && x.Obsoleto == false )
                    .OrderBy(x => x.Nom?.Esp ?? "")
                    .Select(x => new ProductPluginModel.Item
                    {
                        Product = x.Guid,
                        Caption = x.Nom ?? new()
                    }).ToList() ?? new();
            }
            return retval;
        }
        public static ProductPluginModel? CategoryCollection(CacheDTO cache, Guid guid)
        {
            ProductPluginModel? retval = null;
            var category = cache.Category(guid);
            if (category != null)
            {
                retval = new ProductPluginModel(guid);
                retval.Items = cache.Skus
                    .Where(x => x.Category == guid && x.Obsoleto == false)
                    .OrderBy(x => x.Nom.Esp)
                    .Select(x => new ProductPluginModel.Item
                    {
                        Product = x.Guid,
                        Caption = x.Nom
                    }).ToList();
            }
            return retval;
        }

        public static string StoreLocatorCallToActionSnippet()
        {
            return "<!--Store locator CallToAction Plugin---------------><div class='StoreLocatorCallToAction'><a href = '#StoreLocator'></a></div>";
        }

        public static string StoreLocatorCallToActionExpanded(LangDTO lang)
        {
            var template = "<a href = '#StoreLocator'>{0}</a>";
            var label = lang.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar");
            var retval = string.Format(template, label);
            return retval;
        }


        public class Item
        {
            public Guid Product { get; set; }
            public LangTextModel Caption { get; set; } = new();


            public string Expansion(CacheDTO cache, LangDTO lang)
            {
                var sb = new System.Text.StringBuilder();
                var product = cache.ProductFromGuid(Product);
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

                    //var productUrl = cache.ProductUrls.FirstOrDefault(x => x.Target == product.Guid);
                    var href = CanonicalUrl(cache, product, lang); // productUrl?.Url.Tradueix(lang);
                    var caption = Caption.Tradueix(lang) ?? "";
                    sb.AppendLine("<a href='/" + href + "' title='" + caption + "'>");
                    sb.AppendLine(" <div>");
                    sb.AppendLine("     <img src='" + thumbnailUrl + "' width='"+ width +"' height='"+height+"' alt='" + caption + "'/>");
                    sb.AppendLine(" </div>");
                    sb.AppendLine(" <div>" + caption + "</div>");
                    sb.AppendLine("</a>");
                }
                var retval = sb.ToString();
                return retval;
            }

            private string CanonicalUrl(CacheDTO cache, ProductModel product, LangDTO lang)
            {
                var productUrl = cache.ProductUrls.FirstOrDefault(x => x.Target == product.Guid);
                var retval = string.Format("{0}/{1}",lang.Culture2Digits(), productUrl?.Url.Tradueix(lang));
                return retval;
            }

            public override string ToString()
            {
                return Caption?.ToString() ?? "ProductPluginDTO.Item";
            }
        }
    }
}
