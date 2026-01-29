    using System.Net;
    using System.Text.Json;

namespace Identity.Api.Middleware
{

    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var payload = new
            {
                error = ex.GetType().Name,
                message = ex.Message
            };

            var json = JsonSerializer.Serialize(payload);

            return context.Response.WriteAsync(json);
        }
    }
}
