using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ContentRequestDTO
    {
        public string UrlSegment { get; set; }
        public LangDTO? Lang { get; set; }
    }
}
