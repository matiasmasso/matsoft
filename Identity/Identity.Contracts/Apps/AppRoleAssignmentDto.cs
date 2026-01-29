using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Apps
{
    public sealed class AppRoleAssignmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Assigned { get; set; }
    }
}
