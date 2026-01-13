using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSubscriptor : DTOUser
    {
        public DTOSubscription Subscription { get; set; }
        public DateTime FchSignedUp { get; set; }

        public DTOSubscriptor() : base()
        {
        }

        public DTOSubscriptor(Guid oGuid, DTOSubscription oSubscription) : base(oGuid)
        {
            Subscription = oSubscription;
        }

        public DTOSubscriptor(Guid oGuid, DTOSubscription.Wellknowns oWellknownSubscription) : base(oGuid)
        {
            Subscription = DTOSubscription.Wellknown(oWellknownSubscription);
        }

        public static string RecipientsString(List<DTOSubscriptor> oSubscriptors)
        {
            IEnumerable<string> emailAddresses = oSubscriptors.Select(x => x.EmailAddress);
            string retval = string.Join(";", emailAddresses);
            return retval;
        }

        public static List<string> Recipients(List<DTOSubscriptor> oSubscriptors)
        {
            return oSubscriptors.Select(x => x.EmailAddress).ToList();
        }
    }
}
