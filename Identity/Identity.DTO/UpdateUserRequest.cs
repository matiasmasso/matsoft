using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
