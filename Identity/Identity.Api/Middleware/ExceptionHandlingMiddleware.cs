using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace Identity.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString("N");
        context.Items["CorrelationId"] = correlationId;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, correlationId);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, string correlationId)
    {
        context.Response.ContentType = "application/json";

        var (status, code, message) = MapException(ex);

        context.Response.StatusCode = status;

        _logger.LogError(ex,
            "Unhandled exception caught by middleware. Code={Code}, CorrelationId={CorrelationId}",
            code, correlationId);

        var payload = new ApiError
        {
            Code = code,
            Message = message,
            Detail = ex.Message,
            CorrelationId = correlationId
        };

        var json = JsonSerializer.Serialize(payload);

        await context.Response.WriteAsync(json);
    }

    private static (int Status, string Code, string Message) MapException(Exception ex)
    {
        // 1. SQL unavailable (inner SqlException)
        if (ex is InvalidOperationException ioe && ioe.InnerException is SqlException)
        {
            return (
                StatusCodes.Status503ServiceUnavailable,
                "SqlUnavailable",
                "The database is temporarily unavailable."
            );
        }

        // 2. Direct SqlException
        if (ex is SqlException)
        {
            return (
                StatusCodes.Status503ServiceUnavailable,
                "SqlUnavailable",
                "The database is temporarily unavailable."
            );
        }

        // 3. True transient EF Core retryable failures
        if (ex is InvalidOperationException ioe2 &&
            ioe2.Message.Contains("transient", StringComparison.OrdinalIgnoreCase))
        {
            return (
                StatusCodes.Status503ServiceUnavailable,
                "SqlTransientFailure",
                "A transient database failure occurred. Please retry."
            );
        }

        // 4. Other cases...
        return (
            StatusCodes.Status500InternalServerError,
            "ServerError",
            "An unexpected error occurred."
        );
    }

}
