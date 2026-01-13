using MatHelperStd;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOFacebook
    {

        public static string FbImg(string html)
        {
            string retval = "";
            List<string> htmlElements = TextHelper.HtmlElements(html);
            string fbImgElement = htmlElements.FirstOrDefault(x => x.StartsWith("<img ") && x.Contains(" fbimg "));
            if (string.IsNullOrEmpty(fbImgElement))
            {
                fbImgElement = htmlElements.FirstOrDefault(x => x.StartsWith("<img "));
            }
            if (!string.IsNullOrEmpty(fbImgElement))
            {
                retval = TextHelper.srcFromImgTag(fbImgElement);
            }
            return retval;
        }

        public class UserProfile
        {
            public string email { get; set; }
            public string id { get; set; }
            public string first_name { get; set; }
            public string gender { get; set; }
            public string last_name { get; set; }
            public string link { get; set; }
            public string locale { get; set; }
            public string name { get; set; }
            public string username { get; set; }

            public PictureData data { get; set; }

            public class PictureData
            {
                public Picture picture { get; set; }
            }

            public class Picture
            {
                string url { get; set; }
                bool is_silhouette { get; set; }
                int width { get; set; }
                int height { get; set; }

            }

        }
    }
}
