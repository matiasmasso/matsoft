using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop4moms.Services;

namespace Shop4moms.Services
{
    public class HostPageModel : PageModel
    {
        private PostFormService PostFormService { get; }

        // postFormService is injected by the DI
        public HostPageModel(PostFormService postFormService)
        {
            PostFormService = postFormService;
        }

        public void OnPost()
        {
            // store the post form in the PostFormService
            PostFormService.Form = Request.Form;
        }

    }
}
