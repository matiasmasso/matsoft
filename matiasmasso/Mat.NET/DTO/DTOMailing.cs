using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOMailing
    {
        public List<DTOUser> Users { get; set; }
        public List<DTORol> Rols { get; set; }
        public List<DTOArea> Areas { get; set; }

        public static List<DTOLeadChecked> XarxaDistribuidors(List<DTOLeadChecked> src, List<DTODistributionChannel> oChannels, List<DTOProductBrand> oBrands)
        {
            List<DTOLeadChecked> oLeads = new List<DTOLeadChecked>();
            if (oBrands.Count > 0)
            {
                foreach (DTOLeadChecked item in src)
                {
                    if (oBrands.Exists(x => x.Guid.Equals(item.Brand.Guid)))
                    {
                        if (!oLeads.Exists(x => x.EmailAddress == item.EmailAddress))
                        {
                            if (oChannels.Exists(x => x.Equals(item.Contact.ContactClass.DistributionChannel)))
                                oLeads.Add(new DTOLeadChecked() { Guid = item.Guid, EmailAddress = item.EmailAddress, Lang = item.Lang, Checked = true });
                        }
                    }
                }
            }

            List<DTOLeadChecked> retval = oLeads.GroupBy(g => new { g.Guid, g.EmailAddress, g.Lang, g.Checked }).Select(group => new DTOLeadChecked() { Guid = group.Key.Guid, EmailAddress = group.Key.EmailAddress, Lang = group.Key.Lang, Checked = group.Key.Checked }).ToList();
            return retval;
        }
    }
}
