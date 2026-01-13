using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBloggerPost : DTOBaseGuid
    {
        public DTOBlogger Blogger { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Fch { get; set; }
        public DTOLang Lang { get; set; }
        public DateTime HighlightFrom { get; set; }
        public DateTime HighlightTo { get; set; }
        public List<DTOProduct> Products { get; set; }

        public DTOBloggerPost() : base()
        {
            Products = new List<DTOProduct>();
        }

        public DTOBloggerPost(Guid oGuid) : base(oGuid)
        {
            Products = new List<DTOProduct>();
        }

        public static DTOBloggerPost Factory(DTOBlogger oBlogger = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOBloggerPost retval = new DTOBloggerPost();
            {
                var withBlock = retval;
                withBlock.Blogger = oBlogger;
                withBlock.Fch = DTO.GlobalVariables.Today();
            }
            return retval;
        }

        public static string FullUrl(DTOBloggerPost oPost)
        {
            string retval = oPost.Url;
            if (!retval.StartsWith("http"))
                retval = "https://" + retval;
            return retval;
        }
    }
}
