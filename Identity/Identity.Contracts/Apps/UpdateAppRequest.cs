using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Apps
{
    public sealed class UpdateAppRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string? Description { get; set; }
        public bool Enabled { get; set; }
    }
}
