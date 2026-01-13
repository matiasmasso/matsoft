using Test4moms.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Test4moms.Pages
{
    public class HostPageModel:PageModel
    {
        // postFormService is injected by the DI
        public HostPageModel(PostFormService postFormService)
        {
            PostFormService = postFormService;
        }

        private PostFormService PostFormService { get; }

        public void OnPost()
        {
            // store the post form in the PostFormService
            PostFormService.Form = Request.Form;
        }
    }
}
