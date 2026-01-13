
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Shop4moms.LocalComponents;
using Shop4moms.Services;
using Shop4moms.Shared;
using System;
using System.Runtime.Intrinsics.X86;

namespace Shop4moms.Pages
{
    public class _PageBase:_Base
    {
        [Parameter] public string? LangSegment { get; set; }

        [Inject] public NavigationManager? NavigationManager { get; set; }


        protected UserModel? CurrentUser() {
            var auth = (CustomAuthenticationStateProvider?)base.authStateProvider;
            var retval = auth?.CurrentUser;
            return retval;
        }

        protected bool IsEditor()
        {
            var retval = CurrentUser() == null ? false : (CurrentUser()?.Rol == UserModel.Rols.superUser ||
                CurrentUser()?.Rol == UserModel.Rols.marketing);
            return retval;
        } 

        protected bool AllowAnalytics() => !(CurrentUser()?.IsLocal() ?? false);
    }
}
