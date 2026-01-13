using System.Security.Claims;
using System.Security.Principal;

namespace Spa.Extensions
{

    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetId(this ClaimsPrincipal user)
        {
            Guid? retval = null;
            Claim? claim = user.Claims.FirstOrDefault(c => c.Type == "UserData");
            if(claim != null)
            {
                Guid tmp;
                if(Guid.TryParse(claim.Value, out tmp))
                    retval = tmp;
            }

            return retval;
        }
    }
}
