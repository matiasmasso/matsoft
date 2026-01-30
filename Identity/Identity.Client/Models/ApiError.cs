using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Client.Models
{
    public sealed class ApiError
    {
        public string Code { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string? Detail { get; set; }
        public string? CorrelationId { get; set; }
    }

}
