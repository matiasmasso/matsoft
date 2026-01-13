using System;

namespace DTO
{

    public class DTOPost : DTOBaseGuid
    {
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }


        public DTOPost() : base()
        {
        }

        public DTOPost(Guid oGuid) : base(oGuid)
        {
        }
    }

}
