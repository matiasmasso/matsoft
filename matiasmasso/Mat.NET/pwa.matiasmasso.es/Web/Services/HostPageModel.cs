using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Services;

namespace Web.Pages
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
