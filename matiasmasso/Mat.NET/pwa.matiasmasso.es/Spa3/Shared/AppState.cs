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

        public void SetPublicMenuCustomItems(List<MenuItemModel> items)
        {

        }

    }
}
