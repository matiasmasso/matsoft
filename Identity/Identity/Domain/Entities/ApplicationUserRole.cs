using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Identity.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public Guid ApplicationId { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
        public Application Application { get; set; }
    }

}
