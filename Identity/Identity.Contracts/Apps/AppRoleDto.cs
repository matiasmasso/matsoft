using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Apps
{
    public sealed class AppRoleDto
    {
        public Guid Id { get; set; }
        public Guid AppId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
