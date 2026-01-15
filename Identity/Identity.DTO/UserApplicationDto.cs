using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTO
{
public class UserApplicationDto
{
    public Guid ApplicationId { get; set; }
    public string ApplicationName { get; set; } = "";
    public bool IsActive { get; set; }
}
}
