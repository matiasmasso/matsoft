using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBlogger : DTOBaseGuid
    {
        public string Title { get; set; }
        public DTOUser Author { get; set; }
        public string Url { get; set; }
        [JsonIgnore]
        public Byte[] Logo { get; set; } // 150x115
        public List<DTOBloggerPost> Posts { get; set; }
        public bool Obsoleto { get; set; }

        public DTOBlogger() : base()
        {
        }

        public DTOBlogger(Guid oGuid) : base(oGuid)
        {
        }

        public string LogoSegmentUrl()
        {
            return string.Format("Bloggers/logo", base.Guid.ToString());
        }
    }
}
