using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Apps
{
    public sealed class CreateAppRequest
    {
        public string Name { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string? Description { get; set; }
        public bool Enabled { get; set; }

        public string? GoogleClientId { get; set; }
        public string? GoogleClientSecret { get; set; }

        public string? AppleClientId { get; set; }
        public string? AppleClientSecret { get; set; }

        public string? MicrosoftClientId { get; set; }
        public string? MicrosoftClientSecret { get; set; }
    }
}
