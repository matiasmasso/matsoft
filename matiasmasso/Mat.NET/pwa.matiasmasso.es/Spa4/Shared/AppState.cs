using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spa4.Shared
{
    public class AppState
    {
        public Task<AuthenticationState> authenticationState;
        public List<Box> News { get; set; } = new();
        public ProblemDetails? ProblemDetails { get; set; }

        public LangDTO Lang { get; set; }
        public AppState(LangDTO lang)
        {
            Lang = lang;
        }

        public static CacheDTO DefaultCache() => Cache(Globals.DefaultEmp);
        public static List<CacheDTO> Caches { get; set; } = new();
        public static CacheDTO Cache(int empId) {
            var retval = Caches.FirstOrDefault(x => x.EmpId == empId);
            if (retval == null)
            {
                retval = new CacheDTO(empId);
                Caches.Add(retval);
            }
            return retval;
        }


        public void SetPublicMenuCustomItems(List<MenuItemModel> items)
        {

        }

    }
}
