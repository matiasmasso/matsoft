using Components;
using DTO.Integracions.Shop4moms;
using DTO;
using Microsoft.VisualStudio.Threading;
using Components.Services;

namespace Shop4moms.Services
{
    public class CatalogService:ICatalogService
    {
        public CacheDTO Cache { get; set; }
        public bool IsLoaded { get; set; }
        //public event EventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded;
        public AsyncEventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded { get; set; }


        private HttpClient http;

        public CatalogService(HttpClient http)
        {
            this.http = http;
            Task task = Task.Run(async () => await LoadCatalogAsync());
            Cache = new DTO.Integracions.Shop4moms.Cache();
            Cache.Tpv = new DTO.Integracions.Redsys.Tpv(DTO.Integracions.Redsys.Tpv.Ids.Shop4moms, DTO.Integracions.Redsys.Common.Environments.Production);
        }

        public async Task LoadCatalogAsync()
        {
            var apiResponse = await HttpService.GetAsync<Cache>(http, "Shop4moms");
            IsLoaded = apiResponse.Success();
            Cache = apiResponse.Value!;
            Loaded?.DynamicInvoke(this, new DTO.Helpers.MatEventArgs<ProblemDetails>(apiResponse.ProblemDetails));
        }

        //public DTO.Integracions.Redsys.Tpv Tpv() => Cache.Tpv;

        //public decimal? Price(ProductSkuModel sku)
        //{
        //    var rrpp = Cache?.RetailPrices.FirstOrDefault(x => x.Guid == sku.Guid);
        //    var offer = Cache?.Offers.FirstOrDefault(x => x.Guid == sku.Guid);
        //    var retval = offer == null ? rrpp?.Value : offer.Value;
        //    return retval;
        //}

        public string SkuFullNom(ProductSkuModel? sku, LangDTO lang)
        {
            string retval = string.Empty;
            if (sku != null)
            {
                var category = Cache?.Categories.First(x => x.Guid == sku.Category);
                retval = string.Format("{0} {1}", category?.Nom?.Tradueix(lang), sku?.Nom?.Tradueix(lang));
            }
            return retval;
        }

        public string CategoryNom(ProductSkuModel? sku, LangDTO lang)
        {
            string retval = "";
            if (sku != null)
            {
                var category = Cache?.Categories.First(x => x.Guid == sku.Category);
                retval = category?.Nom?.Tradueix(lang);
            }
            return retval;
        }

        //public ProductSkuModel? Sku(Guid? guid) => guid == null ? null : Cache?.Skus.FirstOrDefault(x => x.Guid == guid);
        //public ProductCategoryModel? Category(ProductSkuModel? sku) => sku == null ? null : Category(sku.Category);
        //public ProductCategoryModel? Category(Guid? guid) => guid == null ? null : Cache?.Categories.FirstOrDefault(x => x.Guid == guid);
        //public List<ProductCategoryModel> Categories() => Cache?.Categories.Where(x => x.Codi < 2).ToList() ?? new();
        //public List<ProductSkuModel> Skus(ProductCategoryModel category) => Cache?.Skus.Where(x => x.Category == category.Guid).ToList() ?? new();
        //public string ImageUrl(ProductCategoryModel category) => Globals.ApiUrl("productCategory/image", category.Guid.ToString() + ".jpg");
        //public string? ImageUrl(ProductSkuModel sku) => SkuImageUrl(sku?.Guid);
        //public string? SkuImageUrl(Guid? guid) => guid == null ? null : Globals.ApiUrl("productSku/image", guid.ToString() + ".jpg");

        //public string SkuNom(Guid? guid, LangDTO? lang) => guid == null ? "" : Sku(guid)?.Nom?.Tradueix(lang) ?? "";
        //public string FullNom(ProductSkuModel? sku, LangDTO lang) => sku == null ? "" : string.Format("{0} {1}", Category(sku)?.CamelCase4moms(lang), sku.Nom?.Tradueix(lang));
        //public int Stock(ProductSkuModel? sku) => sku == null ? 0 : Cache?.SkuStocks.FirstOrDefault(x => x.Sku == sku.Guid)?.Stock ?? 0;
        //public bool IsAvailable(ProductSkuModel? sku) => sku == null ? false : Stock(sku) > 0;


    }
}
