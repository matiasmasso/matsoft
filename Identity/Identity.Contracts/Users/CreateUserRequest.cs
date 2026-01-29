using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public sealed class CreateUserRequest
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool Enabled { get; set; }
    }
}
