using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LoginResponse
    {
        public bool IsValid { get; set; }
        public string? Token { get; set; }
    }
}
