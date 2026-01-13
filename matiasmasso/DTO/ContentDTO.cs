using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ContentDTO:BaseGuid
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public LangDTO Lang { get; set; }

        public ContentDTO() : base() { }
        public ContentDTO(Guid guid) : base(guid) { }
    }
}
