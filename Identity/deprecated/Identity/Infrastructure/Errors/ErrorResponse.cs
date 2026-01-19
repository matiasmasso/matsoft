using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Api.Infrastructure.Errors
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; } = new();
    }
}
