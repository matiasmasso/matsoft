using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : Controller
    {


        [HttpPost]
        public IActionResult Post([FromBody] DTO.Excel.Book book)
        {
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var bytes = Helpers.ClosedXml.Bytes(book);
            return File(bytes, contentType, book.Filename);
        }
    }
}
