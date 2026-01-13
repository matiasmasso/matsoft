using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using static DTO.DTOConsumerTicket;

namespace DTO.Helpers
{
    public class UrlHelper
    {
        public static string Product4momsOrElseCanonicalUrl(string src)
        {
            string retval = src;
            var snippetEs =new string[]{ "https://www.matiasmasso.es/4moms/",
            "https://www.matiasmasso.es/es/4moms/",
            "https://www.matiasmasso.es/ca/4moms/",
            "https://www.matiasmasso.es/en/4moms/"};

            var snippetPt = new string[] { "https://www.matiasmasso.pt/4moms/",
                "https://www.matiasmasso.es/pt/4moms/", 
                "https://www.matiasmasso.pt/pt/4moms/" };

            string Es4moms = "https://www.4moms.es/";
            string Pt4moms = "https://www.4moms.pt/";

            foreach (string s in snippetEs)
            {
                if (src.StartsWith(s))
                    retval = src.Replace(s, Es4moms);
            }
            foreach (string s in snippetPt)
            {
                if (src.StartsWith(s))
                    retval = src.Replace(s, Pt4moms);
            }
        return retval;

        }
    }
}
