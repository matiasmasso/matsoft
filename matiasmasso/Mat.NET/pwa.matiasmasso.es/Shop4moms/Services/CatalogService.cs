
using DTO.Integracions.Shop4moms;
using DTO;
using Microsoft.VisualStudio.Threading;
using Shop4moms.Services;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Components;
using Shop4moms.Pages.BackOffice;
using DTO.Helpers;

namespace Shop4moms.Services
{
    public class CatalogService : ICatalogService
    {
        public CacheDTO Cache { get; set; }
        public bool IsLoaded { get; set; }
        public AsyncEventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded { get; set; }
        public event EventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? OnError;

        public async Task ReloadStringsLocalizerAsync()
        {
            var apiResponse = await HttpService.GetAsync<List<StringLocalizerModel>>(http, "StringsLocalizer");
            if (apiResponse.Fail())
                OnError?.Invoke(this, new MatEventArgs<ProblemDetails>(apiResponse.ProblemDetails));
        }


        private HttpClient http;

        public CatalogService(HttpClient http)
        {
            this.http = http;
            //Task task = Task.Run(async () => await LoadCatalogAsync());
            //Cache = new DTO.Integracions.Shop4moms.Cache();
            //Cache.Tpv = new DTO.Integracions.Redsys.Tpv(DTO.Integracions.Redsys.Tpv.Ids.Shop4moms, DTO.Integracions.Redsys.Common.Environments.Production);
        }

        public async Task LoadCatalogAsync()
        {
            var apiResponse = await HttpService.GetAsync<Cache>(http, "Shop4moms");
            IsLoaded = apiResponse.Success();
            Cache = apiResponse.Value!;
            Loaded?.DynamicInvoke(this, new DTO.Helpers.MatEventArgs<ProblemDetails>(apiResponse.ProblemDetails));
        }

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

    }
}
