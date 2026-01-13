using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{

    public static class ExceptionsExtension
    {
        public static ProblemDetails ProblemDetails(this System.Exception ex)
        {
            return new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = ex.Message,
                Detail = ex.InnerException?.Message ?? String.Empty
            };

        }
    }
}
