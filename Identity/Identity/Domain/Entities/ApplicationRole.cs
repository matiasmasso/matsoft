using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Identity.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public Guid? ApplicationId { get; set; }
        public Application Application { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

}
