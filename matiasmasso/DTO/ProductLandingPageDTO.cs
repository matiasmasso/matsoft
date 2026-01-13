using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DTO
{
    public class ProductLandingPageDTO
    {
        public Guid Guid { get; set; }
        public string? Content { get; set; }
        public StoreLocatorDTO StoreLocator { get; set; } = new();

        public ProductLandingPageDTO(Guid guid)
        {
            Guid = guid;
        }

        public List<Box> Breadcrumbs(CacheDTO cache, LangDTO lang)
        {
            var retval = new List<Box>();
            var model = cache.ProductFromGuid(Guid);
            if (model != null)
            {
                if (model.Src == ProductModel.SourceCods.Brand)
                {
                    retval.Add(Box(model,cache, lang));
                }
                else if (model.Src == ProductModel.SourceCods.Dept)
                {
                    var dept = (ProductDeptModel)model;
                    retval.Add(Box(cache.Brand(dept.Brand),cache, lang));
                    retval.Add(Box(model, cache,lang));
                }
                else if (model.Src == ProductModel.SourceCods.Category)
                {
                    var category = (ProductCategoryModel)model;
                    retval.Add(Box(cache.Brand((Guid)category.Brand!), cache,lang));
                    if (category.Dept != null)
                        retval.Add(Box(cache.Dept((Guid)category.Dept), cache, lang));
                    retval.Add(Box(model, cache, lang));
                }
                else if (model.Src == ProductModel.SourceCods.Sku)
                {
                    var sku = (ProductSkuModel)model;
                    var category = cache.Category(sku.Category);
                    retval.Add(Box(cache.Brand((Guid)category!.Brand!), cache, lang));
                    if (category.Dept != null)
                        retval.Add(Box(cache.Dept((Guid)category.Dept), cache, lang));
                    retval.Add(Box(category, cache, lang));
                    retval.Add(Box(model, cache, lang));
                }
            }
            return retval;
        }
        private Box Box(ProductModel? product, CacheDTO cache, LangDTO lang)
        {
            return new Box
            {
                Caption = product?.Nom.Tradueix(lang) ?? "?",
                Url = product?.Guid == Guid ? "" : cache.ProductUrl(product!.Guid, lang)
            };

        }

        public string ContentBeforeMoreOrDefault()
        {
            var retval = string.Empty;
            if (!string.IsNullOrEmpty(Content))
            {
                var label = "<more>";
                var pos = Content.IndexOf(label);
                retval = pos > 0 ? Content.Substring(0, pos) : Content;
            }
            return retval;
        }
        public string ContentAfterMoreOrDefault()
        {
            var retval = string.Empty;
            if (!string.IsNullOrEmpty(Content))
            {
                var label = "<more>";
                var pos = Content.IndexOf(label);
                retval = pos > 0 ? Content.Substring(pos + label.Length) : string.Empty;
            }

            return retval;
        }
    }
}


