using DTO;

namespace Api.Services
{
    public class ProductLandingPageService
    {
        public static ProductLandingPageDTO Fetch(Guid guid, LangDTO lang)
        {
            var retval = new ProductLandingPageDTO(guid);
            var cache = CacheService.CatalogRequest();
            var model = cache.ProductFromGuid(guid);
            if (model != null)
            {
                using (var db = new Entities.MaxiContext())
                {
                    var rawContent = RawContent(db, cache, model, lang);
                    var htmlContent = rawContent?.Html();
                    retval.Content = cache.ExpandPlugins(htmlContent, lang);
                    //retval.Breadcrumbs = BreadCrumbs(db, retval.Product!, lang);
                    retval.StoreLocator = StoreLocatorService.Factory(db, model, lang);
                }
            }
            return retval;
        }

        private static string? RawContent(Entities.MaxiContext db, CacheDTO cache, ProductModel model, LangDTO lang)
        {
            ProductModel? product = model;
            if (model.Src == ProductModel.SourceCods.Sku && ((ProductSkuModel)model).Inherits)
                product = cache.Category(((ProductSkuModel)model)?.Category);
            var loadedProduct = ProductService.Load(db, product);
            var retval = loadedProduct?.Content?.Tradueix(lang);
            return retval;
        }
    }
}
