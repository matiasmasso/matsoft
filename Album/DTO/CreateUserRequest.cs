using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CreateUserRequest
    {
        public string? EmailAddress { get; set; }
        public string? Nickname { get; set; }
        public string? Password { get; set; }
        public UserModel.Rols Rol { get; set; }
    }

}
