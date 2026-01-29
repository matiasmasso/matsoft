using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public sealed class UserAppDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

}
