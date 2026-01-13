using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spa3.Shared;

namespace Spa3.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public void OnPost()
        {
            var emailAddress = Request.Form["emailaddress"];
            // do something with emailAddress
        }

        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            }
            catch { }

            var rc = await ValidateCredentials(paramUsername, paramPassword);
            return LocalRedirect(returnUrl);
        }


        private async Task<bool> ValidateCredentials(string paramUsername, string paramPassword)
        {
            bool retval = false;
            var model = new LoginRequestDTO
            {
                Email = paramUsername,
                Password = paramPassword
            };

            var url = Globals.ApiUrl("user/login");
            var http = new HttpClient();
            var response = await http.PostAsync(url, model, new JsonMediaTypeFormatter());
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(responseText);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                        new Claim(ClaimTypes.Name, user.Nickname ?? user.EmailAddress ?? ""),
                        new Claim(ClaimTypes.Role, ((int?)user.Rol).ToString() ?? "0"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = this.Request.Host.Value
                    };

                    try
                    {
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                        retval = true;
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                }

            }
            return retval;
        }

    }
}