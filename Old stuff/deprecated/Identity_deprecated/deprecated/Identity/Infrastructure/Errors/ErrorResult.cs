using Identity.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Infrastructure.Errors
{
    public static class ErrorResult
    {
        public static IActionResult FromIdentityErrors(IEnumerable<IdentityError> errors)
        {
            var list = errors
                .Select(e => e.Description)
                .ToList();

            return new BadRequestObjectResult(new ErrorResponse { Errors = list });
        }

        public static IActionResult FromReason(string reason)
        {
            return new BadRequestObjectResult(new ErrorResponse
            {
                Errors = new List<string> { reason }
            });
        }

        public static IActionResult FromErrors(IEnumerable<string> errors)
        {
            return new BadRequestObjectResult(new ErrorResponse
            {
                Errors = errors.ToList()
            });
        }

        public static IActionResult FromException(Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponse
            {
                Errors = new List<string> { ex.Message }
            });
        }
    }


}
