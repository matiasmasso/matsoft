using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class FolderModel
    {
        public string? Name { get; set; }

        public string? Path { get; set; }
        public List<FolderModel> SubFolders { get; set; } = new();
        public List<string> Files { get; set; } = new List<string>();
        public int Imgs { get; set; }

    }

    public class FileModel
    {
        public string? Name { get; set; }
        public string? ThumbnailBase64 { get; set; }
    }
}
