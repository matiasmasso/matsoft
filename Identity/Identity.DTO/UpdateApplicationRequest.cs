using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
    public class UpdateApplicationRequest
    {
        public string Name { get; set; } = "";
        public string ClientId { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
