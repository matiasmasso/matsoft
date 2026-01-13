using DTO;
using Microsoft.AspNetCore.Components.Server;
using System;

namespace Web.Services
{
    public class ProductDeptsService
    {
        public List<ProductDeptModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public ProductDeptsService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<ProductDeptModel>>("ProductDepts/1") ?? new();
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

        public List<ProductDeptModel> BrandDepts(ProductBrandModel? brand)
        {
            List<ProductDeptModel> retval = new();
            if (brand != null)
                retval = Values?.Where(x => x.Brand == brand.Guid).ToList() ?? new();
            return retval;
        }

        public ProductDeptModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}
