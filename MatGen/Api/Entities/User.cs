using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class User
    {
        public User()
        {
            Tokens = new HashSet<Token>();
        }

        public Guid Guid { get; set; }
        public string? Nom { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Rol { get; set; }
        public Guid? Person { get; set; }

        public virtual Person? PersonNavigation { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
