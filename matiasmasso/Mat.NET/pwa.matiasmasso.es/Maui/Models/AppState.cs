using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Components;
using Maui.Helpers;

namespace Maui.Models
{
    public class AppState
    {
        //[Inject] public HttpClient http { get; set; }

        public List<NavDTO.Model> MenuItems = new();
        public List<NavDTO.Model> SelectedMenuItems = new();
        public NavDTO? Nav;

        public AppState()
        {
            _ = InitiateCatalogCache();
        }

        public void ResetNav()
        {
            MenuItems = new List<NavDTO.Model>();
            SelectedMenuItems = new List<NavDTO.Model>();
            Nav = null;
        }

        #region "Cache"

        public bool IsLoadingCatalog { get; set; } = false;
        public event EventHandler<EventArgs> CatalogJustLoaded;
        void OnCatalogLoaded()
        {
            // Notify any subscribers to the event
            CatalogJustLoaded?.Invoke(this, new EventArgs());
        }

        private async Task InitiateCatalogCache()
        {
            var httpHelper = new HttpHelper(new HttpClient());
            var cache = AppState.DefaultCache();
            IsLoadingCatalog = true;
            var serverCache = await httpHelper.PostAsync<CacheDTO, CacheDTO>(cache.CatalogRequest(), "cache");

            //update cache if needed
            cache.Merge(serverCache);
            IsLoadingCatalog = false;

            OnCatalogLoaded();
        }

        public static CacheDTO DefaultCache() => Cache(Globals.DefaultEmp);
        public static List<CacheDTO> Caches { get; set; } = new();
        public static CacheDTO Cache(int empId)
        {
            var retval = Caches.FirstOrDefault(x => x.EmpId == empId);
            if (retval == null)
            {
                retval = new CacheDTO(empId);
                Caches.Add(retval);
            }
            return retval;
        }


        #endregion
    }
}
