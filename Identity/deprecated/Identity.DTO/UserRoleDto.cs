using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
    public class UserRoleDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = default!;
        public Guid ApplicationId { get; set; }
    }

}
