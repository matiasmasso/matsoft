using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }


}
