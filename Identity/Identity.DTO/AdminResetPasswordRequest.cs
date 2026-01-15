using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
    public class AdminResetPasswordRequest
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; } = "";
    }
}

