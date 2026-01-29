using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public sealed class UserAppRoleDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = default!;
        public bool Assigned { get; set; }
    }
}
