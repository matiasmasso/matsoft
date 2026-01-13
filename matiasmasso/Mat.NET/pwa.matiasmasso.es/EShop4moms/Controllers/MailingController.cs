
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Shop4moms.Controllers
{
    [Route("[controller]/[action]")]
    public class MailingController : Controller
    {
        public IActionResult test()
        {
            return Ok();
        }

    }
}
