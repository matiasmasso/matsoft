namespace Identity.Api.Middleware
{
    public sealed class ApiError
    {
        public required string Code { get; init; }          // e.g. "SqlUnavailable", "ValidationError"
        public required string Message { get; init; }       // human-readable
        public string? Detail { get; init; }                // optional technical detail
        public string? CorrelationId { get; init; }         // for logs + client support
    }

}
