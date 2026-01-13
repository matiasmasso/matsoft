using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LoginRequestDTO
    {
        public string? Email { get; set; }
        public string? Hash { get; set; }
        public int Emp { get; set; }

        public LoginRequestDTO() { }

        public LoginRequestDTO(string email, string hash) // to deprecate
        {
            Email = email;
            Hash = hash;
        }
    }
}
