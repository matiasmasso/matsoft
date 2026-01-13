using DTO;
using Api.Services;
using Microsoft.AspNetCore.OutputCaching;
using static System.Net.WebRequestMethods;

namespace Api.Shared
{
    public class AppState
    {
        public static List<CacheDTO> Caches { get; set; } = new();
        //allows to reset cachestore
        private readonly IOutputCacheStore _cacheStore;

        public AppState(IOutputCacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        public static CacheDTO Cache(EmpModel.EmpIds empId)
        {
            var retval = Caches.FirstOrDefault(x => x.EmpId == empId);
            if (retval == null)
            {
                retval = new CacheDTO(empId);
                Caches.Add(retval);
            }
            return retval;
        }

        public static CacheDTO DefaultCache() => Cache(EmpModel.EmpIds.MatiasMasso);

        public static void InitiateDefaultCache()
        {
            Cache(EmpModel.EmpIds.MatiasMasso);
        }

        public static bool UseLocalApi = false;
        public static string ApiUrl(params string[] segments)
        {
            var host = UseLocalApi ? "localhost:7111" : "api2.matiasmasso.es";
            var segmentList = new List<string>() { host };
            segmentList.AddRange(segments);
            var retval = string.Format("https://{0}", string.Join("/", segmentList));
            return retval;
        }

        #region CacheStore
        public async Task ResetCacheAsync(CancellationToken cancellationToken)
        {
            // Some logic here....
            await _cacheStore.EvictByTagAsync("tag-all", cancellationToken);
        }
        #endregion
    }


}
