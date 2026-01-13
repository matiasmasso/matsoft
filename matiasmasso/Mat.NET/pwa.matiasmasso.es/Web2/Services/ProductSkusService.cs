using DTO;
using Microsoft.AspNetCore.Components.Server;
using System;

namespace Web.Services
{
    public class ProductSkusService
    {
        public List<ProductSkuModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public ProductSkusService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<ProductSkuModel>>("ProductSkus/1") ?? new();
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

        public ProductSkuModel? FromEan(string? ean) => string.IsNullOrEmpty(ean) ? null : Values?.FirstOrDefault(x => x.Ean == ean);
        public ProductSkuModel? FromId(int? id) => Values?.FirstOrDefault(x => x.Id == id);
        public ProductSkuModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public List<ProductSkuModel>? GetValues(ProductCategoryModel? category, bool onlyActive)
        {
            List<ProductSkuModel>? retval = null;
            var c = Values?.Where(x => x.HasImage && !x.Obsoleto).ToList();
            if (category != null)
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Category == category.Guid && x.Obsoleto == false).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values?.Where(x => x.Category == category.Guid).ToList();
            else
                retval = Values;
            return retval;
        }

        public List<ProductSkuModel>? GetValues(bool onlyActive = true)
        {
            List<ProductSkuModel>? retval = null;
            if (onlyActive)
                retval = Values?
                    .Where(x => x.Obsoleto == false).ToList() ?? new(); //TO DO: filtrar noweb
            else
                retval = Values;
            return retval;
        }

        public List<ProductSkuModel>? GetValues(List<ProductCategoryModel>? categories)
        {
            return Values?.Where(x => categories?.Any(y => x.Category == y.Guid) ?? false).ToList();
        }

        public async Task UpdateAsync(ProductSkuModel? sku)
        {
            if (sku != null)
                await appstate.PostAsync<ProductSkuModel, bool>(sku, "productSku");
        }


    }
}
