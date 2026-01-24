using System.Net;
using System.Text.Json;

namespace Identity.Api.Middleware;

public class JsonExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public JsonExceptionMiddleware(RequestDelegate next)
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
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                error = ex.Message,
                type = ex.GetType().Name
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}