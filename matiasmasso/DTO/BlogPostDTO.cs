using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BlogPostDTO
    {
        public static string RelativeUrl(string? urlFriendly)
        {
            var retval = "blog";
            if (urlFriendly != null)
                retval = string.Format("/blog/{0}.html", urlFriendly!);
            return retval;
        }

    }
}
