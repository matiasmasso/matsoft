using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public sealed class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool Enabled { get; set; }
    }
}
