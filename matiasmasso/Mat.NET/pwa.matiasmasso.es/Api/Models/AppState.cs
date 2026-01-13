using DTO;
using Api.Services;
namespace Api.Models
{
    public class AppState
    {
        public static List<CacheDTO> Caches { get; set; } = new();
        public static CacheDTO Cache(int empId)
        {
            var retval = Caches.FirstOrDefault(x => x.EmpId == empId);
            if(retval == null)
            {
                retval = new CacheDTO(empId);
                Caches.Add(retval);
            }
            return retval;
        }

        public static CacheDTO DefaultCache() => Cache((int)EmpModel.EmpIds.MatiasMasso);

        public static void InitiateDefaultCache()
        {
            Cache((int)EmpModel.EmpIds.MatiasMasso);
        }

        public static bool UseLocalApi = false;
        public static string ApiUrl(params string[] segments)
        {
            var host = UseLocalApi ? "localhost:7198" : "api2.matiasmasso.es";
            var segmentList = new List<string>() { host };
            segmentList.AddRange(segments);
            var retval = string.Format("https://{0}", string.Join("/", segmentList));
            return retval;
        }
    }


}
