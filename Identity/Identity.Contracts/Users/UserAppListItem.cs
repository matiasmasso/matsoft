using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Contracts.Users
{
    public class UserAppListItem
    {
        public Guid AppId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsAssigned { get; set; }
    }
}
