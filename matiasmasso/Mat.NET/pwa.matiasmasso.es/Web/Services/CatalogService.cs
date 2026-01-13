using Components;
using DTO;
using Microsoft.VisualStudio.Threading;
using Components.Services;

namespace Web.Services
{
    public class CatalogService : ICatalogService
    {
        public CacheDTO Cache { get; set; }
        public bool IsLoaded { get; set; }
        public AsyncEventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded { get; set; }


        //private HttpService httpService; singleton cannot consume a scoped service
        private HttpClient http;

        public CatalogService(HttpClient http)
        {
            this.http = http;
            Cache = new CacheDTO(EmpModel.EmpIds.MatiasMasso);
            //Cache.Tpv = new DTO.Integracions.Redsys.Tpv(DTO.Integracions.Redsys.Tpv.Ids.MatiasMasso, DTO.Integracions.Redsys.Common.Environments.Production);
            Task task = Task.Run(async () => await LoadCatalogAsync());
        }

        public async Task LoadCatalogAsync()
        {
            var apiResponse = await HttpService.PostAsync<CacheDTO, CacheDTO>( http, Cache.MmoStartupRequest(), "cache");
            IsLoaded = apiResponse.Success();
            if (apiResponse.Success())
            {
                var serverCache = apiResponse.Value;
                if (serverCache != null)
                    Cache.Merge(serverCache); //update cache if needed
            }

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

        public Task ReloadStringsLocalizerAsync()
        {
            throw new NotImplementedException();
        }
    }
}
