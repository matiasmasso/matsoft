using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public sealed class UpdateUserAppRolesRequest
    {
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
        public List<Guid> RoleIds { get; set; } = new();
    }

}
