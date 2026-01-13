using System.Collections.Generic;

namespace DTO
{
    public class DTOSoftwareDownload
    {
        public DTOLangText Title { get; set; }
        public DTOLangText Excerpt { get; set; }
        public string Url { get; set; }

        public static DTOSoftwareDownload Factory(DTOLangText title, DTOLangText excerpt, string url)
        {
            DTOSoftwareDownload retval = new DTOSoftwareDownload();
            retval.Title = title;
            retval.Excerpt = excerpt;
            retval.Url = url;
            return retval;
        }

        public class Collection : List<DTOSoftwareDownload>
        {

        }
    }
}
