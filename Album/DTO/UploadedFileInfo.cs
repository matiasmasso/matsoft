using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UploadedFileInfo
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }

        public Guid UsrCreated { get; set; }
        public DateTimeOffset FchCreated { get; set; }
    }
}
