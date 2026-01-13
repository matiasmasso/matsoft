using DTO;
using Microsoft.AspNetCore.Components.Server;
using System;
using System.Collections.Generic;

namespace Web.Services
{
    public class ProductCategoriesService
    {
        public List<ProductCategoryModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public ProductCategoriesService(AppStateService appstate)
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
                Values = await appstate.GetAsync<List<ProductCategoryModel>>("ProductCategories/1") ?? new();
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

        public List<ProductCategoryModel>? GetValues(ProductBrandModel? brand, bool onlyActive = true)
        {
            List<ProductCategoryModel>? retval = null;
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Brand == brand?.Guid && x.Obsoleto == false && x.HasImage).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values?.Where(x => x.Brand == brand?.Guid).ToList();
            return retval;
        }
        public List<ProductCategoryModel>? GetValues(bool onlyActive = true)
        {
            List<ProductCategoryModel>? retval = null;
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Obsoleto == false && x.HasImage).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values;
            return retval;
        }

        public List<ProductCategoryModel>? GetValues(ProductDeptModel? dept, bool onlyActive)
        {
            List<ProductCategoryModel>? retval = null;
            if (dept != null)
                if (onlyActive)
                    retval = Values?
                        .Where(x => x.Dept == dept.Guid && x.Obsoleto == false && x.HasImage).ToList() ?? new(); //TO DO: filtrar noweb
                else
                    retval = Values?.Where(x => x.Dept == dept.Guid).ToList();
            else
                retval = Values;
            return retval;
        }

        public ProductCategoryModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

    }
}
